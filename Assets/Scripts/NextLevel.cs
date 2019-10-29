using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NextLevel : MonoBehaviour {

	Collider2D col;
	public GameObject keyBubble;
	public Transform apKeyBubble;
	public bool isLastLevel = false;

	// Use this for initialization
	void Start () {
		col = GetComponent <Collider2D> ();
		apKeyBubble = GameObject.Find ("KeyBubblePos").transform;
	}
		
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player" && other.GetComponent<PlayerController> ().hasKey && !isLastLevel)
			GameManager.instance.WinLevel ();
		else if (other.gameObject.tag == "Player" && other.GetComponent<PlayerController> ().hasKey && isLastLevel)
			GameManager.instance.BackToMenu ();

		//Sent each frame where another object is within a trigger collider attached to this object (2D physics only).

		if (other.gameObject.tag == "Player" && !other.GetComponent<PlayerController> ().hasKey) {

			Destroy (Instantiate (keyBubble, apKeyBubble), 1f);
		}
	}
}
