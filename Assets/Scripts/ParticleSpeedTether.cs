using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpeedTether : MonoBehaviour
{
    
    BoatController boatController;

    private void Awake()
    {
        boat = GameObject.FindWithTag("Player").GetComponent<BoatController>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
