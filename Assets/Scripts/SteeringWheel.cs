using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    public float minAngle = -180;
    public float maxAngle = 180;
    public float currentAngle;

    float deltaAngle;
    BoatController boatController;

    private void Awake()
    {
        boatController = GameObject.FindWithTag("Player").GetComponent<BoatController>();
    }

    void Update()
    {
        deltaAngle = 1f * Mathf.Sin(Time.time * 15) + 3f * Mathf.Sin(Time.time * 2) + 5f * Mathf.Cos(Time.time * 1.2f);

        currentAngle += deltaAngle;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, -(boatController.NormalizedSteering * 340 + deltaAngle * boatController.NormalizedSpeed)), Time.deltaTime * 2);
    }
}
