using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour 
{
	public float xspeed = 0, yspeed = 8f;
	Vector2 _direction; //the direction of the bullet

	// Use this for initialization
	void Start () 
	{
	}

	public void setSpeed(float x, float y)
	{
		xspeed = x;
		yspeed = y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Get the bullet's current positon
		Vector2 position = transform.position;

		//Compute the bullet's new position
		position = new Vector2 (position.x + xspeed, position.y + yspeed * Time.deltaTime); 

		//update the bullet's positon
		transform.position = position;

		//top right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));

		//destroy out of bound bullets
		if (transform.position.y > max.y) 
		{
			Destroy (gameObject);
		}
	}

	public void SetDirection(Vector2 direction)
	{
		_direction = direction.normalized;

		//isReady = true; 	//set flag to true
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//Detect collision of vs enemy ship
		if (col.tag == "EnemyShipTag") 
		{
				Destroy(gameObject);	//Destroy player bullet
		}
	}
}
