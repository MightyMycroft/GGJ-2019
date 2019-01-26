using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    public float minAngle = -180;
    public float maxAngle = 180;
    public float currentAngle;

    private float deltaAngle;

    void Update()
    {
        deltaAngle = 0.1f*Mathf.Sin(Time.time * 15) + 0.3f * Mathf.Sin(Time.time * 2) + 0.5f * Mathf.Cos(Time.time * 1.2f);

        if (Input.GetKey(KeyCode.A))
        {
            currentAngle = Mathf.Lerp(currentAngle, maxAngle, Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentAngle = Mathf.Lerp(currentAngle, minAngle, Time.deltaTime);
        }
        else
        {
            currentAngle = Mathf.Lerp(currentAngle, 0, Time.deltaTime);
        }

        currentAngle += deltaAngle;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, currentAngle), Time.deltaTime * 2);
    }
}
