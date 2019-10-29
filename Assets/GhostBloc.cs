using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBloc : MonoBehaviour {

	//public GameObject limits;
	//public Collider2D myLimits;

	// Use this for initialization
	void Start () {
		//myLimits = limits.GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Debug.Log ("OOOOH");
			GameManager.instance.playerInGhost = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		GameManager.instance.playerInGhost = false;
	}
}
