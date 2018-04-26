using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour 
{
	public GameObject StarGO;	//For the Star Prefab.
	public int MaxStars;  //Maximum number of stars.

	//array of colors
	Color[] starColors = 
	{
		new Color (0.5f, 0.5f, 1f), //blue
		new Color (0, 1f, 1f), //green
		new Color (1f, 1f, 1f), //yellow
		new Color (1f, 0, 0), //red
	};

	// Use this for initialization
	void Start () 
	{
		//bottom left corner
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));

		//top right corner
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));

		for (int i = 0; i < MaxStars; i++) 
		{
			GameObject star = (GameObject)Instantiate (StarGO);

			//Set star color
			star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

			//Set the positoon of the star (random x and random y)
			star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

			//Set a random speed for the star
			star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

			//Make the star a child of the StarGeneratorGO
			star.transform.parent = transform;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
