using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrackingBullet : MonoBehaviour 
{
	float speed; //Enemy bullet speed
	Vector2 _direction; //the direction of the bullet
//	bool isReady; //to know when the bullet direction is set

	//set default values in Awaken function
	void Awake()
	{
		speed = 5f;
//		isReady = false;
	}

	// Use this for initialization
	void Start () 
	{
		
	}

	//Function to set the bullet's direction
	public void SetDirection(Vector2 direction)
	{
		_direction = direction.normalized;

//		isReady = true; 	//set flag to true
	}


	// Update is called once per frame
	void Update () 
	{

		GameObject playerShip = GameObject.Find ("PlayerGO");	//Try to find playship

		if (playerShip != null) {  //if player ship is not dead
			
		}

		//Get the bullet's current position
		Vector2 position = transform.position;

		//compute the bullet's new position
		position += _direction * speed * Time.deltaTime;

		//Update the bullet's position
		transform.position = position;

		//Remove out of bound bullet
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));//bottom left
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));//Top right

		//Destroy out of bound bullet
		if((transform.position.x < min.x) || (transform.position.x > max.x) || 
		   (transform.position.y < min.y) || (transform.position.y > max.y))
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//Detect collision of vs player ship
		if (col.tag == "PlayerShipTag") 
		{
			Destroy(gameObject);	//Destroy enemy bullet
		}
	}
}
