using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public float minAngle = -75;
    public float maxAngle;

    Transform SpeedRod;

    private void Awake()
    {
        SpeedRod = transform.GetChild(0).GetChild(0);
    }

    void Update()
    {
        var input = Input.GetKey(KeyCode.Space);
        SpeedRod.localRotation = Quaternion.Slerp(SpeedRod.localRotation, Quaternion.Euler(input ? maxAngle : minAngle, 0, 0), Time.deltaTime * 1.5f);
    }
}
