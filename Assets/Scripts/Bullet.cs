using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (0f,0f,90f);
	}

	//Sent when an incoming collider makes contact with this object's collider (2D physics only).
	void OnCollisionEnter2D (Collision2D collision2DInfo)
	{
		
		Destroy (gameObject);
	}
}
