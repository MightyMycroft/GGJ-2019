using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public float minAngle = -75;
    public float maxAngle;

    BoatController boatController;

    Transform SpeedRod;

    private void Awake()
    {
        boatController = GameObject.FindWithTag("Player").GetComponent<BoatController>();

        SpeedRod = transform.GetChild(0).GetChild(0);
    }

    void Update()
    {
        SpeedRod.localRotation = Quaternion.Slerp(SpeedRod.localRotation, Quaternion.Euler(minAngle - boatController.NormalizedSpeed * minAngle, 0, 0), Time.deltaTime * 1.5f);
    }
}
