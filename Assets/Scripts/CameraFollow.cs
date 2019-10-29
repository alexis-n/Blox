using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smooth = 5, offset = 2;
	public Vector3 levelCameraBounderyMin, levelCameraBounderyMax;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.Lerp (transform.position, 
			new Vector3 (Mathf.Clamp(target.position.x, levelCameraBounderyMin.x, levelCameraBounderyMax.x), Mathf.Clamp(target.position.y + offset, levelCameraBounderyMin.y, levelCameraBounderyMax.y), transform.position.z), smooth * Time.deltaTime);
	}
}
