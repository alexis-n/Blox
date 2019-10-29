using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRun : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim;
	SpriteRenderer sr;
	BoxCollider2D bcol;
	CircleCollider2D ccol;
	EnemyRun er;

	public GameObject smokePrefab;

	public Sprite block;
	public Sprite enemy;

	[Range (1, 10)]
	public float walkSpeed = 5;

	public Transform groundCheck;
	public Transform wallCheck;

	bool grounded = false;
	bool nearWall = false;
	bool isBlock = false;

	Vector2 startPosition;

	// Use this for initialization
	void Start ()
	{
		startPosition = transform.position;

		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		ccol = GetComponent<CircleCollider2D> ();
		bcol = GetComponent<BoxCollider2D> ();
		sr = GetComponent<SpriteRenderer> ();

		bcol.enabled = false;
	}

	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.R)) {
			Rewind ();
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!isBlock) {
			grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
			nearWall = Physics2D.Linecast (transform.position, wallCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

			float h = 1f;

			if (nearWall || !grounded) {
				walkSpeed = -walkSpeed;
			}

			rb.velocity = new Vector2 (h * walkSpeed, rb.velocity.y);

			anim.SetFloat ("HorizontalSpeed", rb.velocity.x);

			if (walkSpeed < 0 && transform.localScale.x > 0) {
				transform.localScale = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
			}
			if (walkSpeed > 0 && transform.localScale.x < 0) {
				transform.localScale = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D collider)
	{
		if (collider.gameObject.tag == "rayon") {
			TransformToBlock ();
		}
	}

	void TransformToBlock ()
	{
		isBlock = true;
		Destroy (Instantiate (smokePrefab, transform.position, Quaternion.identity), 1f);
		rb.mass = 1000;
		sr.sprite = block;
		anim.enabled = false;
		ccol.enabled = false;
		bcol.enabled = true;
		gameObject.layer = 8;
		gameObject.GetComponent <Rigidbody2D>().velocity = new Vector2 (0,0);
	}

	void Rewind ()
	{
		isBlock = false;
		rb.mass = 1;
		sr.sprite = enemy;
		anim.enabled = true;
		ccol.enabled = true;
		bcol.enabled = false;
		gameObject.layer = 0;
		Destroy (Instantiate (smokePrefab, transform.position, Quaternion.identity), 1f);
	}
}
