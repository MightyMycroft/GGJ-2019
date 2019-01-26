using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Fishing : MonoBehaviour
{
    public string FishTag = "Fish";
    public string HomeTag = "Home";

    public Transform fishCargoRoot;         
    private Transform[] fishCargoSpaces;    // Transform for where to store the fish, the fish gets childed to those transform
    private int cargoIndex;                 // What cargo index should we put the next fish 

    void Start()
    {
        fishCargoSpaces = new Transform[fishCargoRoot.childCount];
        for(int i = 0; i < fishCargoRoot.childCount;i++)
        {
            fishCargoSpaces[i] = fishCargoRoot.GetChild(i);
        }
    }

    void SellFish()
    {
        for(int i = 0; i < fishCargoSpaces.Length;i++)
        {
            // just removing them for now, 
            //TODO add cool selling effect
            // Maybe make it corutine so we can do it like over a few seconds or something, 
            // like starts from the top and goes to bottom in like 1-2 seconds

            if(fishCargoSpaces[i].GetChild(0) != null)
                Destroy(fishCargoSpaces[i].GetChild(0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Hit fish, we caught it
        if(collision.gameObject.tag == FishTag)
        {
            collision.transform.parent = fishCargoSpaces[cargoIndex];
            collision.transform.localPosition = Vector3.zero;
            collision.transform.rotation = Quaternion.identity;

            cargoIndex++;
        }

        // We are home, sell fish
        if(collision.gameObject.tag == HomeTag)
        {
            SellFish();

            cargoIndex = 0;
        }
    }
}
