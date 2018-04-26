using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]


public class MusicManager : MonoBehaviour 
{
	public AudioClip StageMusic;
	public AudioClip BossMusic;

	private AudioSource playing;

// Use this for initialization
//	IEnumerator Start () 
/*void Start () 
	{
		AudioSource audio = GetComponent<AudioSource> ();

		StageMusic.Play ();
		yield return new WaitForSeconds (audio.clip.length);
		audio.clip = MainLoopMusic;
		audio.Play ();
	}
*/

	void Start()
	{
			
	}

	public void PlayStageMusic()
	{
		playing.Stop ();
		playing.PlayOneShot (StageMusic);
	}

	public void PlayBossMusic()
	{
		playing.Stop ();
		playing.PlayOneShot (BossMusic);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
