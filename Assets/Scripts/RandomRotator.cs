using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RandomRotator : MonoBehaviour 
{

	public float tumble;

	void Start()
	{
		var body = GetComponent<Rigidbody> ();
		body.angularVelocity = Random.insideUnitSphere * tumble;
	}

}