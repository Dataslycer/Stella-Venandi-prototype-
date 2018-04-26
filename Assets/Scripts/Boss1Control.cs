using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Control : MonoBehaviour 
{

	GameObject scoreUITextGO;		//Reference to score text UI
	public GameObject ExplosionGO;	//Explosion prefab animation
	public GameObject Gun;			//Reference to enemy gun

	float xspeed = 0f; //Enemy speed
	float yspeed = 1f; //Enemy speed
	public float shield = 800;	//Shield of the enemy ship
	public float health = 800;	//Health of the enemy ship

	bool dead = false;


	// Use this for initialization
	void Start () 
	{
		scoreUITextGO = GameObject.FindGameObjectWithTag ("ScoreTextTag");
		StartCoroutine(Pattern());
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move ();
	}

	IEnumerator Pattern()
	{
		Debug.Log (this.health);				//Debugging
		yield return new WaitForSeconds(2);		//Move forward 2 second
		yspeed = 0;								//Stop moving forward
		while (dead == false) {
			xspeed = 0.02f;												//Move left
			for (float temp = 0; temp < 5; temp++) {
				Gun.GetComponent<EnemyGun> ().FireEnemyBullet (3);		//Fire 3 bullets at players
				yield return new WaitForSeconds(0.8f);					//Delay 0.8 seconds each
			}
			xspeed = -0.02f;
			for (float temp = 0; temp < 5; temp++) {
				Gun.GetComponent<EnemyGun> ().FireEnemyBullet (3);
				yield return new WaitForSeconds(0.8f);
			}
		}
	}

	void Move()
	{
		Vector2 position = transform.position;												//Get the enemy current position
		position = new Vector2 (position.x - xspeed, position.y - yspeed * Time.deltaTime);	//Complete the enemy new position
		transform.position = position;														//Update the enemy position


		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));		//bottom-left screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));		//top-right screen
		if (transform.position.y < min.y || transform.position.y > max.y) yspeed *= -1 ; //Move other direction when bumping into border
		if (transform.position.x < min.x || transform.position.x > max.x) xspeed *= -1 ; 
	}



	void OnTriggerEnter2D(Collider2D col)
	{
		//Detect collision of enemy ship
		if (col.tag == "PlayerBulletTag") 
		{
			shield -= 20;					//Damage to shield
			if (shield <= 0) 				
			{
				health = health + shield;	//Damages past shield and hits armor
				shield = 0;
			}

			if (health <= 0)		//If enemy is out of health
			{
				scoreUITextGO.GetComponent<GameScore>().Score += 500;	//Add 500 points
				StartCoroutine(BossDeath());							//Function for chain explosion.

			}
		}
		if (col.tag == "PlayerShipTag")
		{
			//PlayExplosion();
//			StartCoroutine(BossDeath());
		}
	}

	IEnumerator BossDeath()
	{
		dead = true;
		//this.GetComponent<BoxCollider> ().enabled = false;
//		GameObject explosion = (GameObject)Instantiate (ExplosionGO);
		CancelInvoke("Move");
		for (int temp = 0; temp < 8; temp++) 
		{
			PlayExplosionRandom();
			yield return new WaitForSeconds(0.2f);

		}
		for (int temp2 = 0; temp2 < 4; temp2++) 
		{
			Gun.GetComponent<EnemyGun> ().FireRandomEnemyBullet ();
		}
		Destroy(gameObject);	//Destroy enemy ship
	}


	//Function to instantiate explosion
	void PlayExplosion()
	{
		GameObject explosion = (GameObject)Instantiate (ExplosionGO);
		explosion.transform.position = transform.position;						//Set position of explosion
	}

	void PlayExplosionRandom()
	{
		Vector3 temp = transform.position;
		temp.x += Random.Range (-0.7f, 0.7f);
		temp.y += Random.Range (-1f, 1f);
		GameObject explosion = (GameObject)Instantiate (ExplosionGO);	//Create explosion
		explosion.transform.position = temp;							//Set position of explosion

	}

}
