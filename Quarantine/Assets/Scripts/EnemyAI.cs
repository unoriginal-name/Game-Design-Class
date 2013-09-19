using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	//global variables outlined in the tutorial; in the actual turn-based game, we can probably get rid of these,
	//since there will only be one target -- the player -- and it's turn-based, so move speed is irrelevant.
	public Transform playerPosition;
	public int moveSpeed;
	public int rotationSpeed;
	
	private Transform myTransform;
	
	//this function is run before anything else in the function
	void Awake()
	{
		myTransform = transform; //cache the enemy's position into a variable
	}

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerPosition = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		//movement will go once we're not in real-time, but the enemy should always be facing the player.
		Debug.DrawLine (playerPosition.transform.position, myTransform.position, Color.yellow);
		
		//look at player
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation (playerPosition.position - myTransform.position), rotationSpeed * Time.deltaTime);
	
		//move toward player -- once it's turn-based, it will only do this during its turn.
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
	}
}
