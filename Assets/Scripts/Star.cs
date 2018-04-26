using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour 
{

	public float speed;		//Star's speed

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Get the current positoon of the star
		Vector2 position = transform.position;

		//Compute the star's newposition
		position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

		//Update the star's position
		transform.position = position;

		//bottom left corner
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));

		//top right corner
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));

		//Reposition ot of bound stars
		if (transform.position.y < min.y) 
		{
			transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
		}

	}
}
