using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 120f;            // Maximum speed of the car
    public float acceleration = 5f;         // Acceleration of the car
    public float deceleration = 5f;         // Deceleration (negative acceleration) of the car
    public float brakeForce = 40f;          // Braking force
    public float turnSpeed = 10f;            // Speed of turning
    public SpawnManager spawnManager;

    private float currentSpeed = 0f;

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        MoveCar();
    }

    void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate the car based on the horizontal input
        transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);

        // Accelerate, decelerate, or brake the car based on the vertical input
        if (verticalInput > 0)
        {
            Accelerate();
        }
        else if (verticalInput < 0)
        {
            Decelerate();
        }

        // Brake when the spacebar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            Brake();
        }
    }

    void Accelerate()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = maxSpeed;
        }
    }

    void Decelerate()
    {
        if (currentSpeed > 0)
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = 0;
        }
    }

    void Brake()
    {
        // Apply braking force to slow down the car
        currentSpeed -= brakeForce * Time.deltaTime;

        // Ensure the car doesn't go backward when braking
        currentSpeed = Mathf.Max(0, currentSpeed);
    }

    void MoveCar()
    {
        // Move the car forward based on the current speed
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other){
        spawnManager.SpawnTriggerEntered();
    }
}