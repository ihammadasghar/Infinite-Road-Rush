using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObstacleGenerator: MonoBehaviour {
	
	public List<GameObject> obstacles = null;
	private List<GameObject> spawnedObstacles = new List<GameObject>();
	public int obstaclesToSpawn = 30;

	// Use this for initialization
	void Start () 
	{
		SpawnObstacles();
	}

	private void SpawnObstacles()
    {
        if (obstacles == null)
        {
            Debug.LogError("Obstacles list is not set or empty. Please assign prefabs in the Unity Editor.");
            return;
        }

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            // Choose a random prefab from the array
            GameObject selectedPrefab = obstacles[ProbabilityFunctions.pickDiscreteNumber(3)];

            // Generate a random position within the spawnRadius
            Vector3 randomPosition = new Vector3(
                transform.position.x + ProbabilityFunctions.getRandomCordinate(-100, 100),
                transform.position.y,
                transform.position.z + ProbabilityFunctions.getRandomCordinate(-30, 30)
            );

            // Instantiate the selected prefab at the random position
            GameObject spawnedObstacle = Instantiate(selectedPrefab, randomPosition, Quaternion.identity) as GameObject;
			spawnedObstacles.Add(spawnedObstacle);
        }
    }

    public void ReSpawnObstacles(){
        foreach(GameObject g in spawnedObstacles){
            Destroy(g);
        }

        spawnedObstacles.Clear();
        SpawnObstacles();
    }

}
