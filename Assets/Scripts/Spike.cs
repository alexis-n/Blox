using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
	public GameObject smokePrefab;
	Collider2D col;

	// Use this for initialization
	void Start () {
		col = GetComponent <Collider2D> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			
			if (!other.GetComponent<PlayerController> ().isBlock) {
				Destroy (Instantiate (smokePrefab, other.transform.position, Quaternion.identity),1f);

				other.GetComponent<SpriteRenderer> ().enabled = false;
				other.GetComponent<PlayerController> ().enabled = false;
				GameManager.instance.Dead (2f);
			}
		}
	}
}
