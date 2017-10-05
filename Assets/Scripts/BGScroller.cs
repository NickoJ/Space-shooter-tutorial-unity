using UnityEngine;

public class BGScroller : MonoBehaviour
{

	public float scrollSpeed;
	public float scrollLength;

	Vector3 startPosition;

	void Start()
	{
		startPosition = transform.position;
	}

	void Update()
	{
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, scrollLength);
		transform.position = startPosition + Vector3.forward * newPosition;
	}

}