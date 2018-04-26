using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour 
{

	public GameObject EnemyBulletGO;


	// Use this for initialization
	void Start () 
	{
//		StartCoroutine(firepattern());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
		
	//Function to fire an enemy bullet
	public void FireEnemyBullet()
	{
		//Get a reference to the player's ship
		GameObject playerShip = GameObject.Find ("PlayerGO");

		if (playerShip != null)  //if player ship is not dead
		{ 
			//float angle = 15;
			//Instantiate bullet
			GameObject bullet = (GameObject)Instantiate (EnemyBulletGO);

			//set the bullet's intitial positon
			bullet.transform.position = transform.position;

			//computer the bullet's direction to the player
			Vector2 direction = playerShip.transform.position - bullet.transform.position;

			//set bullet direction
			bullet.GetComponent<EnemyBullet>().SetDirection(direction);
			Debug.Log (playerShip.transform.position +" and " +bullet.transform.position);

			Math instance = new  Math();
			Vector2 blank = new Vector2 (0, 1);
			//Debug.Log(instance.getAngle (blank ,direction));


		}
	}

	//Function to fire 3 enemy bullets
	public void FireEnemyBullet(int bullets)
	{
		//Get a reference to the player's ship
		GameObject playerShip = GameObject.Find ("PlayerGO");

		if (playerShip != null)  //if player ship is not dead
		{ 
			//float angle = 15;
			//Instantiate bullet
			GameObject bullet = (GameObject)Instantiate (EnemyBulletGO);
			GameObject bullet2 = (GameObject)Instantiate (EnemyBulletGO);
			GameObject bullet3 = (GameObject)Instantiate (EnemyBulletGO);

			//set the bullet's intitial positon
			bullet.transform.position = transform.position;
			bullet2.transform.position = transform.position;
			bullet3.transform.position = transform.position;

			//computer the bullet's direction to the player
			Vector2 direction = playerShip.transform.position - bullet.transform.position;
			Vector2 direction2 = playerShip.transform.position - bullet2.transform.position;
			Vector2 direction3 = playerShip.transform.position - bullet3.transform.position;

			direction2.x *= Random.Range (.3f, .8f) ; direction2.y *= Random.Range (.3f, .8f);
			direction3.x *= Random.Range (.3f, .8f) ; direction3.y *= Random.Range (.3f, .8f);
			//			Debug.Log (playerShip.transform.position +" and " +bullet.transform.position);

			//set bullet direction
			bullet.GetComponent<EnemyBullet>().SetDirection(direction);
			bullet2.GetComponent<EnemyBullet>().SetDirection(direction2);
			bullet3.GetComponent<EnemyBullet>().SetDirection(direction3);

//			Debug.Log (playerShip.transform.position +" and " +bullet.transform.position);

		}
	}

	public void FireRandomEnemyBullet()
	{
		//Get a reference to the player's ship
		GameObject playerShip = GameObject.Find ("PlayerGO");

		if (playerShip != null)  //if player ship is not dead
		{ 
			//float angle = 15;
			//Instantiate bullet
			GameObject bullet = (GameObject)Instantiate (EnemyBulletGO);

			//set the bullet's intitial positon
			bullet.transform.position = transform.position;

			//computer the bullet's direction to the player
			Vector2 direction = playerShip.transform.position - bullet.transform.position;


			direction.x = Random.Range (-1f, 1f) ; direction.y *= Random.Range (-1f, 1f);

			//set bullet direction
			bullet.GetComponent<EnemyBullet>().SetDirection(direction);


		}
	}

}
