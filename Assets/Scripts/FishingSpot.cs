using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just a container to tell you which fish you can get here
// Also tells the Fishing Component that it can get any more fish here

public class FishingSpot : MonoBehaviour
{
    public GameObject FishPrefab;
    public bool IsCaught;

    private GameObject _fishRef;

    public void Start()
    {
        
    }

    void Caught()
    {
        IsCaught = true;
        _fishRef = null;

        //TODO Do something visual such that the player knows that there is no more fish to caught here
    }

    private void SpawnFish()
    {
        _fishRef = Instantiate(FishPrefab, transform.position, Quaternion.identity);
    }
}
