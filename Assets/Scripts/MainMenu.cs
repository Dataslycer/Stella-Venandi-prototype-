using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

	public void PlayButton()
	{
		Debug.Log ("Play");
		SceneManager.LoadScene("Mission Control");
	}
	public void LoadButton()
	{
		Debug.Log ("Load");
	}
	public void OptionButton()
	{
		Debug.Log ("Option");
		//OptionMenu.SetActive(true);
	}
	public void QuitButton()
	{
		Debug.Log ("Quit");
		Application.Quit();
	}

}
