using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	Rigidbody2D rb;
	Animator anim;
	SpriteRenderer sr;
	BoxCollider2D co;

	[Range (1, 10)]
	public float walkSpeed = 5;

	[Range (1, 10)]
	public float jumpVelocity = 5;

	public float rotationSpeed = 25f;

	public float fallMultiplier = 2.5f, lowJumpMultiplier = 2f;
	public Transform groundCheck;

	public Sprite block;
	public Sprite player;

	public PhysicsMaterial2D slippery;
	public PhysicsMaterial2D notSlippery;

	public GameObject bulletPrefab;
	public Transform spawnPoint;
	public float bulletSpeed = 50f;

	public GameObject smokePrefab;

	bool grounded = false;
	public bool isBlock = false;

	public bool hasKey = false;
	bool isDead = false;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
		co = GetComponent<BoxCollider2D> ();
		if (GameObject.Find("Key") == null)
		{
			hasKey = true;
		}
	}

	void Update ()
	{
		if (!isBlock) {
			if (Input.GetKeyUp (KeyCode.B)) {
				TransformToBlock ();
			}

			if (Input.GetKeyDown (KeyCode.C)) {
				Shoot ();
			}
		}

		if (isBlock) {
			if (Input.GetKeyUp (KeyCode.R)) {
				Rewind ();
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetButtonDown ("Jump") && grounded) {
			if (!isBlock) {
				rb.velocity = new Vector2 (rb.velocity.x, 1 * jumpVelocity);
				anim.SetBool ("IsJumping", true);
			}
		}

		if (!isBlock) {
			float h = Input.GetAxis ("Horizontal");

			rb.velocity = new Vector2 (h * walkSpeed, rb.velocity.y);

			anim.SetFloat ("HorizontalSpeed", rb.velocity.x);

			if (h < 0 && transform.localScale.x > 0) {
				transform.localScale = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
			}
			if (h > 0 && transform.localScale.x < 0) {
				transform.localScale = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
			}

			if (rb.velocity.y <= -0.1) {
				anim.SetBool ("IsFalling", true);
				anim.SetBool ("IsJumping", false);
				rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
			} else if (rb.velocity.y >= 0.1 && !Input.GetButton ("Jump")) {
				anim.SetBool ("IsJumping", true);
				anim.SetBool ("IsFalling", false);
				rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
			} else if (-0.1 < rb.velocity.y && rb.velocity.y < 0.1) {
				anim.SetBool ("IsJumping", false);
				anim.SetBool ("IsFalling", false);
			}
		} else {
			RotateBlock ();
		}
	}

	void Shoot ()
	{
		float h = Input.GetAxis ("Horizontal");

		GameObject bullet = Instantiate (bulletPrefab, spawnPoint.position, Quaternion.identity);

		bullet.GetComponent<Rigidbody2D> ().velocity = transform.localScale.normalized * bulletSpeed;

		Destroy (bullet, 5);
	}

	void TransformToBlock ()
	{
		Destroy (Instantiate (smokePrefab, transform.position, Quaternion.identity), 1f);

		gameObject.layer = 8;

		anim.enabled = false;
		isBlock = true;

		walkSpeed = 2;

		sr.sprite = block;

		rb.mass = 20;
		rb.constraints = RigidbodyConstraints2D.None;

		co.offset = new Vector2 (0f, 0f);
		co.size = new Vector2 (2.6f, 2.6f);
		co.sharedMaterial = notSlippery;

		transform.position = new Vector2 (transform.position.x, transform.position.y + 2);
	}

	void Rewind ()
	{
		GameObject smoke = Instantiate (smokePrefab, transform.position, Quaternion.identity);
		Destroy (smoke, 1f);
		
		anim.enabled = true;
		isBlock = false;

		gameObject.layer = 0;

		walkSpeed = 5;

		sr.sprite = player;

		rb.mass = 1;
		rb.angularVelocity = 0;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;

		co.offset = new Vector2 (0f, .73f);
		co.size = new Vector2 (1.44f, 1.46f);
		co.sharedMaterial = slippery;

		transform.rotation = Quaternion.identity;
	}

	void RotateBlock ()
	{
		float h = Input.GetAxis ("Horizontal");
		rb.angularVelocity = -h * rotationSpeed;
	}

	//Sent when an incoming collider makes contact with this object's collider (2D physics only).
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Key") {
			coll.gameObject.SetActive (false);
			hasKey = true;
		}

		if (coll.gameObject.tag == "enemy") {
			gameObject.SetActive (false);
			isDead = true;
		}
	}
}
