using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField]
    private Transform endingPoint;

    private CameraMovement cameraScript;

    [SerializeField]
    private Vehicule vehicule;

    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float minRelaunch;
    [SerializeField]
    private float maxRelaunch;

    private float speed;

    void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();

        // Parfois on change les positions pour varier
        if (Random.Range(0f, 1f) >= 0.5f)
        {
            Vector3 currentPos = transform.position;
            transform.position = endingPoint.position;
            endingPoint.position = currentPos;
        }

        float speedAdjustement = transform.position.y / 100;

        speed = Random.Range(minSpeed + speedAdjustement, maxSpeed + speedAdjustement);

        StartCoroutine(LaunchCars());
    }

    IEnumerator LaunchCars()
    {
        while (cameraScript.IsVisibleInCamera(transform.position))
        {
            StartCoroutine(LaunchTrains());

            Vehicule newCar = Instantiate(vehicule, transform.position, Quaternion.identity, transform.parent);
            newCar.InstantiateVehicule(endingPoint, speed);

            if (Random.Range(0, 1) > 0.95)
                yield return new WaitForSeconds(maxRelaunch * 3);
            else
                yield return new WaitForSeconds(Random.Range(minRelaunch, maxRelaunch));
        }

        Destroy(transform.parent.gameObject);
    }

    protected virtual IEnumerator LaunchTrains()
    {
        yield return null;
    }
}
