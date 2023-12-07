using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    RoadSpawner roadSpawner;
    // Start is called before the first frame update
    void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTriggerEntered(Collider other){
        if(other.gameObject.tag == "Player"){
            roadSpawner.MoveRoad();
        }
    }
}
