using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

	Collider2D col;
	public GameObject breakBits, smokePrefab;

	// Use this for initialization
	void Start () {
		col = GetComponent <Collider2D> ();
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player" && other.gameObject.layer == 8) {
			Destroy (Instantiate (breakBits, transform.position, Quaternion.identity), 1f);
			GameObject mySmoke = Instantiate (smokePrefab, transform.position, Quaternion.identity);
			mySmoke.transform.localScale = new Vector3 (5, 5, 5);
			Destroy (mySmoke, 1f);
			Destroy (gameObject);
		}
	}
}
