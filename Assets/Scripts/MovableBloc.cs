using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBloc : MonoBehaviour {

	public Vector3 minLimit, maxLimit;
	public Transform initialTransform;
	public Color initialColor;
	public Vector3 initialPos, initialScale;
	public GameObject myParticles, blocGhost;
	public bool ImRewinded = true, playerInGhost = false;

	public GameObject myGhost;
	Collider2D ghostCol;

	void Start () {
		myGhost = Instantiate (blocGhost, transform.position, Quaternion.identity);
		initialTransform = transform;
		initialPos = initialTransform.position;
		initialScale = initialTransform.localScale;
		initialColor = GetComponent<SpriteRenderer> ().color;
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minLimit.x, maxLimit.x), Mathf.Clamp (transform.position.y, minLimit.y, maxLimit.y), transform.position.z);
	}
		

	public void Rewinded () {
			Destroy (Instantiate (myParticles, transform), 2f);
			GetComponent<SpriteRenderer> ().color = initialColor;
			ImRewinded = true;
	}
}
