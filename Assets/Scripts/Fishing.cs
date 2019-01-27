using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Fishing : MonoBehaviour
{
    public string FishTag = "Fish";
    public string HomeTag = "Dock";

    public AudioClip[] reelingClip;

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

        cargoIndex = 0;
    }

    void SellFish()
    {
        for(int i = 0; i < fishCargoSpaces.Length;i++)
        {
            // just removing them for now, 
            //TODO add cool selling effect
            // Maybe make it corutine so we can do it like over a few seconds or something, 
            // like starts from the top and goes to bottom in like 1-2 seconds

            if(fishCargoSpaces[i].childCount != 0)
                Destroy(fishCargoSpaces[i].GetChild(0).gameObject);
        }

        cargoIndex = 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        // Fish Caught
        if(collider.gameObject.tag == FishTag)
        {
            Debug.Log("Fish Caught!");

            AudioSource.PlayClipAtPoint(reelingClip[Random.Range(0, reelingClip.Length)], Camera.main.transform.position);

            collider.transform.parent = fishCargoSpaces[cargoIndex];
            collider.transform.localPosition = Vector3.zero;
            collider.transform.rotation = Quaternion.identity;

            collider.gameObject.GetComponent<FishMover>().enabled = false;

            cargoIndex++;
        }

        // We are home, sell fish
        if (collider.gameObject.tag == HomeTag)
        {
            Debug.Log("Selling Fish!");

            SellFish();

            cargoIndex = 0;
        }
    }
}
