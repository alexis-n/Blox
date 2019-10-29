using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	#region Singleton

	public static GameManager instance;

	void Awake ()
	{
		if (instance != null) {
			Debug.LogWarning ("More than one instance of GameManager found!");
		}
		instance = this;
	}

	#endregion Singleton

	GameObject[] movablesBlocs;
	public Fading fader;
	public bool playerInGhost;

	// Use this for initialization
	void Start ()
	{
		movablesBlocs = GameObject.FindGameObjectsWithTag ("Bloc");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.R)) {
			RewindBlocs ();
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			UIManager.instance.PauseGame (true);
		}
	}

	public void RewindBlocs ()
	{
		for (int i = 0; i < movablesBlocs.Length; i++) {
			MovableBloc bloc = movablesBlocs [i].GetComponent<MovableBloc> ();
			if (bloc.ImRewinded && playerInGhost == false) {
				Collider2D col = movablesBlocs [i].GetComponent<Collider2D> ();
				bloc.ImRewinded = false;
				SpriteRenderer spriteRend = movablesBlocs [i].GetComponent<SpriteRenderer> ();
				col.enabled = false;
				spriteRend.color = Color.white;
				movablesBlocs [i].transform.DOMove (movablesBlocs [i].GetComponent<MovableBloc> ().initialPos, 0.5f)
				.SetEase (Ease.InOutSine)
				.OnComplete (() => col.enabled = true);
				Sequence mySequence = DOTween.Sequence ();
				mySequence.Append (movablesBlocs [i].transform.DOScale (movablesBlocs [i].transform.localScale / 10, 0.25f))
				.Append (movablesBlocs [i].transform.DOScale (bloc.initialScale, 0.25f).SetEase (Ease.OutBounce))
				.OnComplete (() => bloc.Rewinded ());
			}
		}
	}

	public void WinLevel ()
	{
		string nextLevel;
		if (SceneManager.GetActiveScene ().buildIndex < 10)
			nextLevel = ("Level0" + (SceneManager.GetActiveScene ().buildIndex));
		else
			nextLevel = ("Level" + (SceneManager.GetActiveScene ().buildIndex));
		PlayerPrefs.SetInt ("levelReached", SceneManager.GetActiveScene ().buildIndex);
		Debug.Log (PlayerPrefs.GetInt ("levelReached"));
		fader.FadeTo (nextLevel);
	}

	public void BackToMenu () {
		fader.FadeTo ("MainMenu");
	}

	public void Restart ()
	{
		Time.timeScale = 1;
		fader.FadeTo (SceneManager.GetActiveScene ().name);
	}

	public void Dead (float deathTimer)
	{
		StartCoroutine (Death (deathTimer));
	}

	IEnumerator Death (float deathTimer)
	{
		yield return new WaitForSeconds (deathTimer);
		fader.FadeTo (SceneManager.GetActiveScene ().name);
	}
}
