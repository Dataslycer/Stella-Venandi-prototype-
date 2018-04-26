using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour 
{
	public GameObject[] Planets; //An array of planetGO prefabs

	//Queues of planet
	Queue<GameObject> availablePlanets = new Queue<GameObject>();

	// Use this for initialization
	void Start () 
	{
		availablePlanets.Enqueue (Planets [0]);
		availablePlanets.Enqueue (Planets [1]);
		availablePlanets.Enqueue (Planets [2]);

		//Call the movePlanetdown function every 20 seconds
		InvokeRepeating("MovePlanetDown", 0, 20f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//Dequeue planet an set isMoving flag to true and makes it scroll down
	void MovePlanetDown()
	{
		EnqueuePlanets ();

		//if Queue is empty then return
		if (availablePlanets.Count == 0)
			return;

		//get a planet from queue
		GameObject aPlanet = availablePlanets.Dequeue();

		//set the planet isMoving flag to true
		aPlanet.GetComponent<Planet>().isMoving = true;
	}

	//Enqueue planets below the screen and not moving
	void EnqueuePlanets()
	{
		foreach (GameObject aPlanet in Planets) 
		{
			//if planet is below the screen and not moving
			if ((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet> ().isMoving)) 
			{
				//reset planet position
				aPlanet.GetComponent<Planet>().ResetPosition();

				//Enqueue planet
				availablePlanets.Enqueue(aPlanet);
			}

		}
	}

}
