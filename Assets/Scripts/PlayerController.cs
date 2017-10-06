using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{

	public float speed = 10;
	public float tilt;
	public Boundary boundary;

	public TouchPad touchPad;
	public TouchArea touchArea;

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

	void Start()
	{
		Prepare ();
	}

	void Update()
	{
		if (CanFire() && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}
	}

	void FixedUpdate()
	{
		Vector2 move = GetMove ();

		var movement = new Vector3(move.x, 0f, move.y);
		body.velocity = movement * speed;

		body.position = new Vector3 (
			Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax),
			0f, 
			Mathf.Clamp(body.position.z, boundary.zMin, boundary.zMax)
		);
		body.rotation = Quaternion.Euler (0f, 0f, body.velocity.x * -tilt);
	}

	#if UNITY_ANDROID || UNITY_IOS

	private Quaternion calibrationQuaternion;

	void Prepare()
	{
		CalibrateAccelerometer ();
	}

	bool CanFire()
	{
		return touchArea.CanFire ();
	}

	void CalibrateAccelerometer()
	{
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAcceleration(Vector3 acceleration)
	{
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

	Vector2 GetMove()
	{
		if (touchPad != null) return GetTouchPadMove ();
		else return GetAcceleratorMove ();
	}

	Vector2 GetTouchPadMove()
	{
		return touchPad.GetDirection ();
	}

	Vector2 GetAcceleratorMove()
	{
		return FixAcceleration (Input.acceleration);
	}

	#else

	void Prepare()
	{
		
	}

	bool CanFire()
	{
		return Input.GetButton ("Fire1");
	}

	Vector2 GetMove()
	{
		return new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	#endif

}
