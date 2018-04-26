using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour 
{
	public float speed;		//Speed of the planet
	public bool isMoving;	//flag to make the planet scroll down

	Vector2 min;	//Bottom right screen
	Vector2 max;	//Top left screen


	void Awake()
	{
		isMoving = false;

		min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		//add the planet sprite half height to max y
		max.y = max.y + GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;

		//add the planet sprite half height to max y
		min.y = min.y - GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;

	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isMoving)
			return;

		//get the current position of the planet
		Vector2 position = transform.position;

		//Compute the planet's new position
		position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

		//Update the planet's positon
		transform.position = position;

		//stop moving the planet at the minimum y position
		if (transform.position.y < min.y) 
		{
			isMoving = false;
		}
	}

	//reset planet position
	public void ResetPosition()
	{
		transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
	}
}
