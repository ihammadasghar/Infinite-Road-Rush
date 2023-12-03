using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
    public SpawnManager spawnManager;
    private bool enter = false;

    public void OnTriggerEnter(Collider other){
        
        if(!enter){
            enter = true;
            spawnManager.SpawnTriggerEntered();
        }
        
    }

    public void OnTriggerExit(Collider other){
        
        if(enter){
            enter = false;
        }
        
    }
}