using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

	public float smoothing = 0.1f;

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	private bool touched;
	private int pointerId;

	void Awake()
	{
		#if !UNITY_ANDROID && !UNITY_IOS
		Destroy(this.gameObject);
		#else
		origin = Vector2.zero;
		direction = Vector2.zero;
		touched = false;
		#endif
	}

	void IPointerDownHandler.OnPointerDown (PointerEventData eventData)
	{
		if (touched) return;
		touched = true;
		pointerId = eventData.pointerId;
		origin = eventData.position;
	}

	void IDragHandler.OnDrag (PointerEventData eventData)
	{
		if (eventData.pointerId != pointerId) return;

		Vector2 currentPos = eventData.position;
		Vector2 directionRaw = currentPos - origin;
		direction = directionRaw.normalized;
	}

	void IPointerUpHandler.OnPointerUp (PointerEventData eventData)
	{
		if (eventData.pointerId != pointerId) return;

		direction = Vector2.zero;
		touched = false;
	}

	public Vector2 GetDirection()
	{
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing * Time.deltaTime);
		return smoothDirection;
	}

}