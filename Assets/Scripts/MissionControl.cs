using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionControl : MonoBehaviour 
{
	public int level {get;set;}
	public GameObject PreviewImage;

	public void L1(){this.level = 1;}
	public void L2(){this.level = 2;}
	public void L3(){this.level = 3;}
	public void L4(){this.level = 4;}
	public void L5(){this.level = 5;}
	public void L6(){this.level = 6;}


	public void BackButton()
	{
		Debug.Log ("Back");
		SceneManager.LoadScene("Menu");
	}
	public void AcceptButton()
	{
		Debug.Log ("Accept");
		switch (level) 
		{
		case 1:
			Debug.Log ("Mission 1");
			SceneManager.LoadScene ("Mission1");
			break;
		case 2:
			Debug.Log ("Mission 2");
			SceneManager.LoadScene ("Mission2");
			break;
		}
	}

}
