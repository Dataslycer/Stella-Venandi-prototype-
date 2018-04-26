using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour 
{

	public GameObject GameManagerGO;	//Reference to game manager

	public GameObject PlayerBulletGO;	//This is our player's bullet prefab
	public GameObject bullet;			//
	public GameObject leftButtonSpawn;
	public GameObject rightButtonSpawn;
	public GameObject centerButtonSpawn;
	public GameObject ExplosionGO;	//Explosion prefab animation
	public ScriptableObject Test;

	const int maxHealth = 100;
	const int maxShield = 100;
	public float speed;			//Speed of the player ship
	public float shield = 90;	//Shield of the player ship
	public float health = 100;	//Health of the player ship
	public float shieldRegenRate = 5;	//Rate in which the shield recharges

	//Health Reference
	public Text LivesUIText;
	public Text ShieldUIText;

	int weaponSlot = 1;		//What weapon slot is the player using
	bool firing = false;			//Is play firing on
	float fireCD;

	public void Init()
	{
		health = maxHealth;
		shield = maxShield;

		//Update lives in UI text
		LivesUIText.text = health.ToString ();
		ShieldUIText.text = shield.ToString ();

		//Reset player position to the center upon start
		transform.position = new Vector2(0,0);

		//Set this play game to active
		gameObject.SetActive (true);

		InvokeRepeating ("ShieldRegen", 5, 1);
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float x = Input.GetAxisRaw ("Horizontal");	//for left/right input
		float y = Input.GetAxisRaw ("Vertical");	//for down/up input

		//Computer directional vector, normalize to get unit vector
		Vector2 direction = new Vector2 (x,y).normalized;

		//Call function to set player's position
		Move (direction);

		//Fires bullet when space is pressed
		if (Input.GetKeyDown ("space")) 
		{
			if (firing == true)
				firing = false;
			else
				firing = true;
		}
		fireCD -= 1;
		if (firing == true && fireCD < 1) 
		{
			//Testing running functions from other scripts
			//ModularTest instance = new  ModularTest ();
			//instance.Test ();	

			//plays sound
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
//			GetComponent<AudioSource>.Play();
			switch(weaponSlot)
			{
			case 1:
				GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGO);		//Instantiate bullet
				bullet01.transform.position = centerButtonSpawn.transform.position;	//set bullet initial position
				fireCD = 5;
				break;
			case 2:
				bullet01 = (GameObject)Instantiate (PlayerBulletGO);				//Instantiate bullet
				bullet01.transform.position = leftButtonSpawn.transform.position;	//set bullet initial position
				bullet01 = (GameObject)Instantiate (PlayerBulletGO);				//Instantiate bullet
				bullet01.transform.position = rightButtonSpawn.transform.position;	//set bullet initial position
				fireCD = 10;
				break;
			case 3:
				bullet01 = (GameObject)Instantiate (PlayerBulletGO);				//Instantiate bullet
				bullet01.transform.position = centerButtonSpawn.transform.position;	//set bullet initial position

				bullet01 = (GameObject)Instantiate (PlayerBulletGO);				//Instantiate bullet
				bullet01.transform.position = leftButtonSpawn.transform.position;	//set bullet initial position
				bullet01.GetComponent<PlayerBullet> ().setSpeed(-0.025f, 8);

				bullet01 = (GameObject)Instantiate (PlayerBulletGO);				//Instantiate bullet
				bullet01.transform.position = rightButtonSpawn.transform.position;	//set bullet initial position
				bullet01.GetComponent<PlayerBullet> ().setSpeed(0.025f, 8);
				fireCD = 20;
				//				bullet01.transform.Rotate(35f,35f,0f);
				break;
			}
		}

		if (Input.GetKeyDown (KeyCode.O))
		{
			if (weaponSlot == 1)
				weaponSlot = 3;
			else
				weaponSlot--;
		}
		if (Input.GetKeyDown (KeyCode.P))
		{
			if (weaponSlot == 3)
				weaponSlot = 1;
			else
				weaponSlot++;
		}
	}


	void Move(Vector2 direction)
	{
		//Find the screen limits to the player's movement
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));	//Bottom left
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));	//Top right

		max.x = max.x - 0.225f;		//Subtract the player's sprite half width
		min.x = min.x + 0.225f;		//Add the player's sprite half width

		max.y = max.y - .0285f;		//Subtract the player's sprite half height
		min.y = min.y - .0285f;		//Add the player's sprite half height

		//Get player's position
		Vector2 pos = transform.position;

		//Calculate new positon
		pos += direction * speed * Time.deltaTime;

		//Make sure player's new position is not out of screen
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		//Update player position
		transform.position = pos;
	}

	GameObject FireBullet(GameObject bullet)
	{
		GameObject obj = (GameObject)Instantiate (bullet, centerButtonSpawn.transform.position, centerButtonSpawn.transform.rotation);
		return obj;
	}

	void FireBullet(GameObject bullet, int amount, float spreadAngle)
	{
		float perBulletAngle = spreadAngle / (amount - 1);
		float startAngle = spreadAngle * -0.5f;

		for (int i = 0; i < amount; i++)
		{
			GameObject obj = FireBullet (bullet);
			obj.transform.Rotate (Vector3.up, startAngle + i * perBulletAngle);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//Detect collision of player ship
		if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag")) 
		{	
			shield -= 20;
			if (shield <= 0) 
			{
				health = health + shield;
				shield = 0;
			}
				
			ShieldUIText.text = shield.ToString ();	//Update lives UI text
			LivesUIText.text = health.ToString ();	//Update lives UI text

			if (health <= 0)		//If player is out of health
			{
				health = 0;
				PlayExplosion();
				//Destroy(gameObject);	//Destroy player ship

				//hides ship
				gameObject.SetActive(false);

				//Change game manager state to gameover
				GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
			}
		}
	}


	//Function to instantiate explosion
	void PlayExplosion()
	{
		GameObject explosion = (GameObject)Instantiate (ExplosionGO);

		//Set position of explosion

		explosion.transform.position = transform.position;
	}

	void ShieldRegen()
	{	
		if (shield < maxShield)
		{
			shield += shieldRegenRate;
			if (shield > maxShield)
				shield = maxShield;
			ShieldUIText.text = shield.ToString ();	//Update lives UI text
		}
	}

}
