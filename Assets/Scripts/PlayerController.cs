using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

	public float speed = 10;
	public float tilt;
	public Boundary boundary;

	Rigidbody body;

	void Awake()
	{
		body = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		var movement = new Vector3(moveHorizontal, 0f, moveVertical);
		body.velocity = movement.normalized * speed * Time.deltaTime;

		body.position = new Vector3 (
			Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax),
			0f, 
			Mathf.Clamp(body.position.z, boundary.zMin, boundary.zMax)
		);
		body.rotation = Quaternion.Euler (0f, 0f, body.velocity.x * -tilt);
	}

}
