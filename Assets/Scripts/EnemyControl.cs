using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour 
{

	public GameObject ExplosionGO;	//Explosion prefab animation
	GameObject scoreUITextGO;	//Reference to score text UI
	public GameObject Gun;			//Reference to the gun the enemy is using

	float speed; //Enemy speed
	public float shield = 40;	//Shield of the enemy ship
	public float health = 40;	//Health of the enemy ship


	// Use this for initialization
	void Start () 
	{


		Gun.GetComponent<EnemyGun> ().FireEnemyBullet ();

		speed = 2f; //initial speed
		scoreUITextGO = GameObject.FindGameObjectWithTag ("ScoreTextTag");
	}
	
	// Update is called once per frame
	void Update () 
	{

		//Get the eemy current position
		Vector2 position = transform.position;

		//Complete the enemy new position
		position = new Vector2 (position.x, position.y - speed * Time.deltaTime);

		//Update the enemy position
		transform.position = position;

		//bottom-left screen
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

		//Destroy out of bound enemy
		if (transform.position.y < min.y) 
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//Detect collision of enemy ship
		if (col.tag == "PlayerBulletTag") 
		{
			shield -= 20;
			if (shield <= 0) 
			{
				health = health + shield;
				shield = 0;
			}

			if (health <= 0)		//If enemy is out of health
			{
				PlayExplosion();
				scoreUITextGO.GetComponent<GameScore>().Score += 100;	//Add 100 points

				Destroy(gameObject);	//Destroy enemy ship
			}
		}
		if (col.tag == "PlayerShipTag")
		{
			PlayExplosion();
			Destroy(gameObject);	//Destroy enemy ship
		}
	}

	//Function to instantiate explosion
	void PlayExplosion()
	{
		//Create explosion animation
		GameObject explosion = (GameObject)Instantiate (ExplosionGO);

		//Set position of explosion
		explosion.transform.position = transform.position;
	}
}
