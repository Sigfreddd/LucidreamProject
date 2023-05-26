using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionProbability : MonoBehaviour
{
    [Header("PortionProbability")]
    [SerializeField] private float basicPortionPb;
    [SerializeField] private float roadPortionPb;
    [SerializeField] private float railPortionPb;
    [SerializeField] private float emptyPortionPb;

    [Header("PortionSizeProbability")]
    [SerializeField] private int basicPortionSizePb;
    [SerializeField] private int roadPortionSizePb;
    [SerializeField] private int railPortionSizePb;
    [SerializeField] private int emptyPortionSizePb;

    [Header("ElementsProbabilty")]
    [SerializeField] private float stumpPb;
    [SerializeField] private float molePb;

    [Header("Others")]
    [SerializeField] private TilesGenerator tilesGenerator;

    public void GeneratePortion()
    {
        float myPb = Random.Range(0f, basicPortionPb + roadPortionPb + railPortionPb + emptyPortionPb + 0.999f);

        if (myPb < basicPortionPb)
        {
            tilesGenerator.GenerateBasicPortion(GeneratePortionSize(basicPortionSizePb));
        }
        else if (myPb < basicPortionPb + roadPortionPb)
        {
            tilesGenerator.GenerateRoadPortion(GeneratePortionSize(roadPortionSizePb));
        }
        else if (myPb < basicPortionPb + roadPortionPb + railPortionPb)
        {
            tilesGenerator.GenerateRailPortion(GeneratePortionSize(railPortionSizePb));
        }
        else
        {
            tilesGenerator.GenerateEmptyPortion(GeneratePortionSize(emptyPortionSizePb));
        }
    }

    private int GeneratePortionSize(int maxSize)
    {
        return (int)Random.Range(1f, maxSize + 0.999f);
    }

    public bool[] GenerateSafeZones(int lastSafeZone, int size)
    {
        bool[] safeZones = new bool[size];

        for (int i = 0; i < size; i++)
        {
            if (Random.Range(0f, 1f) >= stumpPb || i == lastSafeZone)
                safeZones[i] = true;
            else
                safeZones[i] = false;
        }

        return safeZones;
    }
    
    public bool[] GenerateMoleZones(bool[] safeZones, int size)
    {
        bool[] moleZones = new bool[size];

        for (int i = 0; i < size; i++)
        {
            if (Random.Range(0f, 1f) <= molePb && safeZones[i])
                moleZones[i] = true;
            else
                moleZones[i] = false;
        }

        return moleZones;
    }
}
