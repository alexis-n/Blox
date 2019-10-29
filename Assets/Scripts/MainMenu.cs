using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public string levelSelect = "LevelSelect";
	public Fading fader;
	public bool eraseSave = false;

	public void Play () {
		if (eraseSave)
		{
			PlayerPrefs.SetInt ("levelReached", 1);
			eraseSave = false;
		}
		fader.FadeTo (levelSelect);
	}

	public void Quit () {
		Debug.Log ("Quitting...");
		Application.Quit ();
	}


}
