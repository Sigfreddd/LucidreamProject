using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform transf;
    [SerializeField]
    private Transform playerTransf;

    [SerializeField]
    private float yoffset;

    private float cameraX;
    private float cameraZ;
    private float cameraY;

    [SerializeField]
    private float cameraLimitX;
    [SerializeField]
    private float cameraLimitY;

    void Start()
    {
        cameraX = transform.position.x;
        cameraY = playerTransf.position.y;
        cameraZ = transform.position.z;
    }

    void FixedUpdate()
    {
        if (playerTransf != null)
        {
            float playerY = playerTransf.position.y;

            /*if (!IsInCamera(playerTransf.position))
                Debug.Log("The player is fleeing !");*/

            if (playerY + yoffset > cameraY)
            {
                transf.position = new Vector3(cameraX, playerY + yoffset, cameraZ);
                cameraY = playerY + yoffset;
            }
        }
    }

    public bool IsInCamera(Vector3 pos)
    {
        return pos.x <= cameraX + cameraLimitX && pos.x >= cameraX - cameraLimitX
            && pos.y <= cameraY + cameraLimitY && pos.y >= cameraY - cameraLimitY;

    }

    public bool IsVisibleInCamera(Vector3 pos)
    {
        return pos.y > cameraY - cameraLimitY - 2;

    }

    public float GetCameraLimitY()
    {
        return cameraLimitY;
    }
}
