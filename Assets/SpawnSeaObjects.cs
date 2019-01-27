using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSeaObjects : MonoBehaviour
{

    private float timer = 0;
    public float minSeconds = 2;
    public float maxSeconds = 7;
    public float distance;
    public List<GameObject> objects;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }


    private void CheckForSpawn()
    {
        if(timer > minSeconds)
        {
            SpawnObject();
        }

        if(timer > maxSeconds)
        {
            ResetTimer();
        }
    }

    public void ResetTimer()
    {
        timer = 0;
    }

    private void SpawnObject()
    {
        int spawnId = (int)Random.Range(0f, objects.Count);
        float randomModifier = Random.Range(-1 * distance, distance);
        Vector3 newPosition = new Vector3(transform.position.x + randomModifier, transform.position.y, transform.position.z + randomModifier);
        Instantiate(objects[spawnId], newPosition, Quaternion.Euler(0,Random.Range(0,360), 0));
    }
}
