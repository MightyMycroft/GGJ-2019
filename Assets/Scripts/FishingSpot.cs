using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just a container to tell you which fish you can get here
// Also tells the Fishing Component that it can get any more fish here

public class FishingSpot : MonoBehaviour
{
    public GameObject FishPrefab;
    public Transform FishRootContainer;

    private void SpawnFish()
    {
        if(FishRootContainer.childCount == 0)
            Instantiate(FishPrefab, FishRootContainer);
    }
}
