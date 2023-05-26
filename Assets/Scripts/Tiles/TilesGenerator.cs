using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesGenerator : MonoBehaviour
{
    [Header("TileMaps")]
    [SerializeField] private Tilemap groundTileMap;
    [SerializeField] private Tilemap obstacleTileMap;
    [SerializeField] private Tilemap decoTileMap;

    [Header("Tiles")]
    [SerializeField] private Tile basicGroundTile;
    [SerializeField] private Tile roadGroundTile;
    [SerializeField] private Tile railGroundTile;
    [SerializeField] private Tile stumpTile;
    [SerializeField] private Tile moleTile;

    [Header("Important Imports")]
    [SerializeField] private SectionProbability sectionProbability;
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private GameObject railPrefab;

    [Header("Values")]
    [SerializeField] private int screenWidthMin;
    [SerializeField] private int screenWidthMax;

    private int lastY;
    private int lastSafeZone;

    private bool lastIsEmptyPortion = true;

    void Start()
    {
        lastY = 0;

        GenerateEmptyPortion(3);

        GenerateBasicPortion(Random.Range(3, 5));

        GenerateEmptyPortion(1);

        GenerateRoadPortion(Random.Range(1, 3));

        GenerateEmptyPortion(1);

        GenerateRailPortion(Random.Range(1, 2));

        GenerateEmptyPortion(1);
    }

    // PORTION = multiple lines of the same type
    // LINE = 1 line (same Y)

    // ----- GENERATION -----
    public void GeneratePortion()
    {
        if (!lastIsEmptyPortion && lastY < 60) // Permets de laisser des ?carts entre les portions en d?but de jeu pour le rendre + facile
        {
            GenerateEmptyPortion(1);
            lastIsEmptyPortion = true;
        }
        else
        {
            sectionProbability.GeneratePortion();
            lastIsEmptyPortion = false;
        }
    }

    // PORTION VIDE
    public void GenerateEmptyPortion(int howMuch)
    {
        for (int i = 1; i <= howMuch; i++)
        {
            FillLineGround(basicGroundTile);
            lastY++;
        }
        lastIsEmptyPortion = true;
    }

    // PORTION NORMALE
    public void GenerateBasicPortion(int howMuch)
    {
        for (int i = 1; i <= howMuch; i++)
        {
            FillLineGround(basicGroundTile);

            // GENERATION SOUCHES
            int screenSize = GetTailleEcran();
            bool[] safeZones = sectionProbability.GenerateSafeZones(lastSafeZone, screenSize); // On g?n?re des "safe zones"
            GenerateStumps(safeZones);

            FindNewSafeZone(safeZones); // On change la "safe zone"

            // GENERATION TAUPES
            bool[] moleZones = sectionProbability.GenerateMoleZones(safeZones, screenSize); // On g?n?re des "mole zones"
            GenerateMoles(moleZones);

            lastY++;
        }
    }

    // PORTION ROUTE
    public void GenerateRoadPortion(int howMuch)
    {
        for (int i = 1; i <= howMuch; i++)
        {
            FillLineGround(roadGroundTile);
            Instantiate(roadPrefab, new Vector3(0f, lastY - 0.1f, 0f), Quaternion.identity, transform);
            lastY++;
        }
    }

    // PORTION TRAIN
    public void GenerateRailPortion(int howMuch)
    {
        for (int i = 1; i <= howMuch; i++)
        {
            FillLineGround(basicGroundTile); // d'abord le sol
            for (int j = screenWidthMin; j <= screenWidthMax; j++) // puis les rails
            {
                Vector3Int newTilePos = new(j, lastY, 0);
                decoTileMap.SetTile(newTilePos, railGroundTile);
            }
            Instantiate(railPrefab, new Vector3(6.5f, lastY + 0.375f, 0f), Quaternion.identity, transform);
            lastY++;
        }
    }

    // ----- UTILITAIRE GENERATION -----
    private void FillLineGround(Tile tile)
    {
        for (int i = screenWidthMin; i <= screenWidthMax; i++)
        {
            Vector3Int newTilePos = new(i, lastY, 0);
            groundTileMap.SetTile(newTilePos, tile);
        }
    }

    private void FindNewSafeZone(bool[] safeZones)
    {
        if (Random.Range(0f, 1f) <= 0.5f)
            FindNewSafeZoneStartingLeft(safeZones);
        else
            FindNewSafeZoneStartingRight(safeZones);
    }

    private void GenerateStumps(bool[] safeZones)
    {
        for (int i = screenWidthMin; i <= screenWidthMax; i++)
        {
            if (!safeZones[i - screenWidthMin])
            {
                Vector3Int newTilePos = new(i, lastY, 0);
                obstacleTileMap.SetTile(newTilePos, stumpTile);
            }
        }
    }

    private void GenerateMoles(bool[] moleZones)
    {
        for (int i = screenWidthMin; i <= screenWidthMax; i++)
        {
            if (moleZones[i - screenWidthMin])
            {
                Vector3Int newTilePos = new(i, lastY, 0);
                decoTileMap.SetTile(newTilePos, moleTile);
            }
        }
    }

    private void FindNewSafeZoneStartingLeft(bool[] safeZones)
    {
        if (lastSafeZone > 0)
        {
            if (safeZones[lastSafeZone - 1])
                lastSafeZone--;
            else
            {
                if (lastSafeZone < safeZones.Length - 1)
                {
                    if (safeZones[lastSafeZone + 1])
                        lastSafeZone++;
                    else
                    {
                        return;
                    }
                }
            }
        }
    }

    private void FindNewSafeZoneStartingRight(bool[] safeZones)
    {
        if (lastSafeZone < safeZones.Length - 1)
        {
            if (safeZones[lastSafeZone + 1])
                lastSafeZone++;
            else
            {
                if (lastSafeZone > 0)
                {
                    if (safeZones[lastSafeZone - 1])
                        lastSafeZone--;
                    else
                    {
                        return;
                    }
                }
            }
        }
    }

    // ----- GETTER -----

    public int GetLastY()
    {
        return lastY;
    }

    // ----- UTILITAIRE -----
    private int GetTailleEcran()
    {
        return Mathf.Abs(screenWidthMin) + Mathf.Abs(screenWidthMax) + 1;
    }
}
