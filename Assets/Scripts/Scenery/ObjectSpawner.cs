using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

	public GameObject obj;
    [Range(0f, 2f)] public float min;
    [Range(2f, 4f)] public float max;

    public List<GameObject> objsSpawned { get; private set; }

    void Start()
	{
        objsSpawned = new List<GameObject>();
        Invoke("SpawnLevel", Random.Range(min, max));
    }

	public void SpawnLevel()
	{
		objsSpawned.Add(Instantiate (obj, transform.position, Quaternion.identity, transform));
        Invoke("SpawnLevel", Random.Range(min, max));
	}
}
