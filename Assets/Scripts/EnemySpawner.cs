using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
	public GameObject EnemyGO;	//The Enemy Prefab the spawn creates
	public GameObject BossGO;	//The Boss Enemy Prefab the spawn creates

	float maxSpawnRateInSeconds = 5f;
	float timeUntilBoss = 30f;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//Function to spawn enemy
	void SpawnEnemy()
	{
		//Bottom left
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		//Top right
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		float random = 0;

		while (random < 0.2) {
			//Instantiate enemy
			GameObject anEnemy = (GameObject)Instantiate (EnemyGO);
			anEnemy.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);

			//Schedule when to spawn next enemy
			random = Random.value;
		}
		ScheduleNextEnemySpawn ();
	}

	void SpawnBoss()
	{
		//Bottom left
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		//Top right
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		GameObject BossEnemy = (GameObject)Instantiate (BossGO);
		BossEnemy.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
		//Invoke ("SpawnBoss", timeUntilBoss);
		CancelInvoke ("SpawnEnemy");
	}

	void ScheduleNextEnemySpawn()
	{
		//Reset max spawn rate
		maxSpawnRateInSeconds = 5f;

		float spawnInNSeconds;

		if (maxSpawnRateInSeconds > 1f) 
		{
			//Pick a number from 1 to maxSpawnRateInSeconds
			spawnInNSeconds = Random.Range (1f, maxSpawnRateInSeconds);
		} 
		else
			spawnInNSeconds = 1f;

		Invoke ("SpawnEnemy", spawnInNSeconds);

	}

	//function to increase difficult of the game
	void IncreaseSpawnRate()
	{
		if (maxSpawnRateInSeconds > 1f)
			maxSpawnRateInSeconds--;
		if (maxSpawnRateInSeconds == 1f)
			CancelInvoke ("IncreaseSpawnRate");

	}

	//function to start enemy spawner
	public void ScheduleEnemySpawner()
	{
		//Initialize enemy spawn
		Invoke ("SpawnEnemy", maxSpawnRateInSeconds);
		Invoke ("SpawnBoss", timeUntilBoss);

		//Increase spawn rate every 30 seconds
		InvokeRepeating ("IncreaseSpawnRate", 0f, 30f);
	}

	//function to stop enemy spawning
	public void UnscheduleEnemySpawner()
	{
		CancelInvoke ("SpawnEnemy");
		CancelInvoke ("SpawnBoss");
		CancelInvoke ("IncreaseSpawnRate");
	}

}
