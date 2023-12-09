using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    public float offset = 150f;
    public Transform player;
    public float spawnDistance = 50f;

    // Start is called before the first frame update
    void Start()
    {
        if(roads != null && roads.Count > 0){
            roads = roads.OrderBy(r=>-(r.transform.position.x)).ToList();
        }
        
    }

    void Update()
    {
        // Check the player's position and spawn a new road segment if needed
        if (player.position.x < roads[1].transform.position.x - spawnDistance)
        {
            MoveRoad();
        }
    }

    public void MoveRoad(){
        GameObject moveRoad = roads[0];
        roads.Remove(moveRoad);
        float newX = roads[roads.Count -1].transform.position.x - offset;
        moveRoad.transform.position = new Vector3(newX, 0, 0);
        moveRoad.gameObject.GetComponent<ObstacleGenerator>().ReSpawnObstacles();
        roads.Add(moveRoad);
    }
}
