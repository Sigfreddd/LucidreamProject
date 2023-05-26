using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float basicMoveSpeed;

    [SerializeField]
    private Transform movePoint;

    [SerializeField]
    private LayerMask cantMoveThere;

    [SerializeField]
    private CameraMovement cameraScript;

    [SerializeField]
    private TextSetter stepCounterTextSetter;

    [SerializeField] private TilesGenerator tilesGenerator;

    private int maxSteps = 0;
    private int steps = 0;

    private void Start()
    {
        movePoint.parent = null;
    }

    void FixedUpdate()
    {
        // On gere la vitesse qui doit etre differente en vertical car pas la meme taille
        float moveSpeed = basicMoveSpeed;
        if (Mathf.Abs(movePoint.position.x - transform.position.x) > .05f)
            moveSpeed *= 2;
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // Input de joueur
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            float horizAxis = Input.GetAxisRaw("Horizontal");
            float vertAxis = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(horizAxis) == 1f)
                Move(new Vector3(horizAxis * 2, 0f, 0f));

            else if (Mathf.Abs(vertAxis) == 1f)
                Move(new Vector3(0f, vertAxis, 0f));
        }
    }

    private void Move(Vector3 movementVector)
    {
        Vector3 newPos = movePoint.position + movementVector;

        // On verifie si pas d'obstacles ou de sortie de camera
        if (!Physics2D.OverlapCircle(newPos, 0.2f, cantMoveThere) && cameraScript.IsInCamera(newPos))
        {
            movePoint.position = newPos;

            // On compte les pas
            steps += (int)movementVector.y;
            if (steps > maxSteps)
            {
                maxSteps = steps;
                stepCounterTextSetter.UpdateText(maxSteps); // On ajuste le compteur de distance

                // On demande ? g?nerer le chemin devant si besoin
                if (tilesGenerator.GetLastY() <= maxSteps + cameraScript.GetCameraLimitY() + 3)
                {
                    tilesGenerator.GeneratePortion();
                }
            }
        }
    }

    public int GetMaxSteps()
    {
        return maxSteps;
    }
}
