using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{

	public float speed = 10;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	Rigidbody body;
	AudioSource audioSource;
	float nextFire;

	void Awake()
	{
		body = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void Update()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		var movement = new Vector3(moveHorizontal, 0f, moveVertical);
		body.velocity = movement * speed * Time.deltaTime;

		body.position = new Vector3 (
			Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax),
			0f, 
			Mathf.Clamp(body.position.z, boundary.zMin, boundary.zMax)
		);
		body.rotation = Quaternion.Euler (0f, 0f, body.velocity.x * -tilt);
	}

}
