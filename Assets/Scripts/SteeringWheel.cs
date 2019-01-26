using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    BoatController playerBoat;

    public void Awake()
    {
        playerBoat = GameObject.FindWithTag("Player").GetComponent<BoatController>();
    }

    void Update()
    {
        transform.localRotation =Quaternion.Euler(0, 0, playerBoat.CurrentTorque * 300);
    }
}
