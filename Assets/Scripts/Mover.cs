using UnityEngine;

public class Mover : MonoBehaviour
{

	public float speed = 1f;

	Rigidbody body;

	void Awake()
	{
		body = GetComponent<Rigidbody> ();
	}

	void Start()
	{
		body.velocity = transform.forward * speed;
	}

}	