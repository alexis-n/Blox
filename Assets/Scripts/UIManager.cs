using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.ImageEffects;

public class UIManager : MonoBehaviour
{
	#region Singleton

	public static UIManager instance;

	void Awake ()
	{
		if (instance != null) {
			Debug.LogWarning ("More than one instance of UIManager found!");
		}
		instance = this;
	}

	#endregion Singleton

	public GameObject pauseMenu;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void PauseGame (bool yesno)
	{
		if (yesno == true)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
		pauseMenu.SetActive (yesno);
		CameraBlur ();

	}

	public void Quit ()
	{
		Debug.Log ("Quitting...");
		Application.Quit ();
	}

	public void CameraBlur ()
	{
		if (Time.timeScale == 0) {
			DOTween.To (() => Camera.main.GetComponent <BlurOptimized> ().blurSize, x => Camera.main.GetComponent <BlurOptimized> ().blurSize = x, 6f, 1f).SetUpdate (true);
			DOTween.To (() => Camera.main.GetComponent <BlurOptimized> ().blurIterations, x => Camera.main.GetComponent <BlurOptimized> ().blurIterations = x, 2, 1f).SetUpdate (true);
		}
		if (Time.timeScale > 0) {
			DOTween.To (() => Camera.main.GetComponent <BlurOptimized> ().blurSize, x => Camera.main.GetComponent <BlurOptimized> ().blurSize = x, 0, 1f).SetUpdate (true);
			DOTween.To (() => Camera.main.GetComponent <BlurOptimized> ().blurIterations, x => Camera.main.GetComponent <BlurOptimized> ().blurIterations = x, 0, 1f).SetUpdate (true);
		}
	}
}
