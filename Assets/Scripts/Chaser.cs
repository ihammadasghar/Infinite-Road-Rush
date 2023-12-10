using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class Chaser : MonoBehaviour {
	
	public float speed = 0;
	public float minDist = 1f;
	public Transform target;

	// Use this for initialization
	void Start () 
	{
		// if no target specified, assume the player
		if (target == null) {

			if (GameObject.FindWithTag ("Player")!=null)
			{
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}

		if(speed == 0){
			speed = ProbabilityFunctions.getEnemySpeed(GameManager.enemySpeedUpperLimit, GameManager.enemySpeedIntervalLen, GameManager.enemySpeedNormalBias);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target == null)
			return;

		// face the target
		transform.LookAt(target);

		//get the distance between the chaser and the target
		float distance = Vector3.Distance(transform.position,target.position);

		//so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
		if(distance > minDist)	
			transform.position += transform.forward * speed * Time.deltaTime;	
	}

	// Set the target of the chaser
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

	public void SetSpeed(float s){
		speed = s;
	}

}
