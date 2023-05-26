using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : Vehicule
{
    public int maxWagons;

    public GameObject wagon;
    public BoxCollider2D colliderB;

    void Awake()
    {
        int howManyWagons = (int)Random.Range(1, maxWagons);

        for (int i = 1; i <= howManyWagons; i++)
        {
            float wagonX = transform.position.x;
            if (wagonX < 1f)
            {
                wagonX -= 2 + 2 * i;
                colliderB.offset = new Vector2(-Mathf.Abs(colliderB.offset.x), colliderB.offset.y);
            }
            else
                wagonX += 2 * i;

            Instantiate(wagon, new Vector3(wagonX, transform.position.y, 0f) , Quaternion.identity, transform);
        }
    }
}
