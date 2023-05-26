using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicule : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer sprite;

    private Transform endingPoint;

    private float moveSpeed;

    private bool hasBeenInstantiate;

    void Start()
    {
        if (transform.position.x > 1f)
            sprite.flipX = false;
        else
            sprite.flipX = true;
    }

    public void InstantiateVehicule(Transform ending, float speed)
    {
        endingPoint = ending;
        moveSpeed = speed;

        hasBeenInstantiate = true;
    }

    void FixedUpdate()
    {
        if (hasBeenInstantiate)
        {
            if (Mathf.Abs(transform.position.x - endingPoint.position.x) < 0.5f)
                Destroy(gameObject);
            else
                transform.position = Vector3.MoveTowards(transform.position, endingPoint.position, moveSpeed * Time.deltaTime);
        }
    }
}
