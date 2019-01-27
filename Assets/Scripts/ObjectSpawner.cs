using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	[SerializeField] GameObject[] prefabsToSpawn;
	[SerializeField] int poolSize;
	[SerializeField] Transform spawnPoint;
	[SerializeField] float spawnWidth;

	GameObject[][] prefabPools;
	int[] lastUsed;

	private void Awake() {
		prefabPools = new GameObject[prefabsToSpawn.Length][];
		lastUsed = new int[prefabsToSpawn.Length];

		for(int i = 0; i < prefabsToSpawn.Length; i++) {
			var prefab = prefabsToSpawn[i];
			prefabPools[i] = new GameObject[poolSize];

			for(int j = 0; j < poolSize; j++) {
				var gameObject = Instantiate(prefab, transform);
				gameObject.SetActive(false);
				prefabPools[i][j] = gameObject;
			}
		}
	}

	private void Start() {
		SpawnObject(20);
	}

	GameObject GetRandomObject()
	{
		int randomObject = Random.Range(0, prefabsToSpawn.Length);
		int index = lastUsed[randomObject];
		lastUsed[randomObject] = index++;

		var gameObject = prefabPools[randomObject][index];

		return gameObject;
	}

	Vector3 GetRandomLocation()
	{
		var pos = spawnPoint.position;
		var random = (Random.value - .5f) * 2 * spawnWidth;
		return pos + spawnPoint.right * random;	
	}

	public void SpawnObject(int numberOfObjects){
		for(int i = 0; i < numberOfObjects; i++) {
			var gameObject = GetRandomObject();
			gameObject.transform.position = GetRandomLocation();
			gameObject.SetActive(true);
		}
	}
}
