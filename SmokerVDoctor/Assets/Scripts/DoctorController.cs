using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorController : MonoBehaviour {


	public float speed;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {


	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");



		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);

		rb.AddForce (movement * speed);
	}
}
