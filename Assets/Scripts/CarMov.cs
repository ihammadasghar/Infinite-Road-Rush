using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMov : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    private int currentRoadIndex = 1; // Start with the middle road

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            int prev = currentRoadIndex;
            currentRoadIndex = Mathf.Clamp(currentRoadIndex + 1, 0, 2);
            if(prev != currentRoadIndex){
                rb.transform.position = transform.position + new Vector3(0.0f, 0.15f, 5.1f);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            int prev = currentRoadIndex;
            currentRoadIndex = Mathf.Clamp(currentRoadIndex - 1, 0, 2);
            if(prev != currentRoadIndex){
                rb.transform.position = transform.position + new Vector3(0.0f, 0.15f, -5.1f);
            }
        }
    }
}


