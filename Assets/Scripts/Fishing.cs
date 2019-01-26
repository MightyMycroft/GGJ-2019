using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Fishing : MonoBehaviour
{
    public string CollisionFishingSpotTag = "FishingSpot";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == CollisionFishingSpotTag)
        {

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == CollisionFishingSpotTag)
        {

        }
    }
}
