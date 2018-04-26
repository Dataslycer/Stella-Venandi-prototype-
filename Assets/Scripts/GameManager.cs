using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

	//Reference to game objects
	public GameObject playButton;
	public GameObject playerShip;
	public GameObject enemySpawner;
	public GameObject GameOverGO;
	public GameObject scoreUITextGO;

	public float numEnemies = 0;
	public float numBoss = 0;

	public enum GameManagerState
	{
		Opening,
		Gameplay,
		GameOver,
	}

	GameManagerState GMState;

	// Use this for initialization
	void Start () 
	{
		SetGameManagerState (GameManagerState.Gameplay);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//Function to update the game manager state
	void UpdateGameManagerState()
	{
		switch (GMState) {
		case GameManagerState.Opening:
			//Hide game over
			GameOverGO.SetActive(false);

			//Set play button visible
			playButton.SetActive(true);

			break;
		case GameManagerState.Gameplay:
			//Reset score (not used)
			//scoreUITextGO.GetComponent<GameScore>().Score = 0;

			//Hide play button
			playButton.SetActive (false);

			//Set playship visible and init player value
			playerShip.GetComponent<PlayerControl>().Init();

			//Start enemy spawner
			enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

			break;
		case GameManagerState.GameOver:
			//Stop Enemy spawner
			enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

			//Display game over
			GameOverGO.SetActive(true);
			//Change game manager state to Opening state after 8 seconds
			Invoke("ChangeToOpeningState", 8f);

			break		;
		}

	}
	public void SetGameManagerState(GameManagerState state)
	{
		GMState = state;
		UpdateGameManagerState ();
	}

	//Play button will call this function
	public void StartGamePlay()
	{
		GMState = GameManagerState.Gameplay;
		UpdateGameManagerState ();
	}

	//Function to change game manager state to opening state
	public void ChangeToOpeningState()
	{
		SetGameManagerState (GameManagerState.Opening);
	}
}
