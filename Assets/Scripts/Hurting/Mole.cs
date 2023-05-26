using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteR;

    [SerializeField]
    private BoxCollider2D moleCollider;

    public Sprite inGroundSprite;
    public Sprite alertSprite;
    public Sprite outOfGroundSprite;

    public float minStart;
    public float maxStart;
    public float minStop;
    public float maxStop;

    public float alertTime;

    private CameraMovement cameraScript;

    void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();

        StartCoroutine(StartMoling());
    }

    IEnumerator StartMoling()
    {
        while (cameraScript.IsVisibleInCamera(transform.position))
        {
            spriteR.sprite = inGroundSprite;
            moleCollider.enabled = false;

            yield return new WaitForSeconds(Random.Range(minStart, maxStart));

            spriteR.sprite = alertSprite;

            yield return new WaitForSeconds(alertTime);

            spriteR.sprite = outOfGroundSprite;
            moleCollider.enabled = true;

            yield return new WaitForSeconds(Random.Range(minStop, maxStop));
        }

        Destroy(gameObject);
    }
}
