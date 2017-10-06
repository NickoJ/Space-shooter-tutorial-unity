using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	private bool touched;
	private int pointerId;

	private bool canFire;

	void Awake()
	{
		#if !UNITY_ANDROID && !UNITY_IOS
		Destroy(this.gameObject);
		#else
		touched = false;
		#endif
	}

	void IPointerDownHandler.OnPointerDown (PointerEventData eventData)
	{
		if (touched) return;
		touched = true;
		canFire = true;
		pointerId = eventData.pointerId;
	}

	void IPointerUpHandler.OnPointerUp (PointerEventData eventData)
	{
		if (eventData.pointerId != pointerId) return;

		canFire = false;
		touched = false;
	}

	public bool CanFire()
	{
		return canFire;
	}
		
}