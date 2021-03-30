using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeftStickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image stickHolder;
	private Image analogStick;

	public Vector3 inputDirection { set; get; }
	public bool isDrag;

	void OnEnable() {
		SetInitialReferences ();
	}

	void SetInitialReferences() {
		stickHolder = GetComponent<Image> ();
		analogStick = transform.GetChild (0).GetComponent<Image> ();
		inputDirection = Vector3.zero;
	}

	public virtual void OnDrag(PointerEventData ped) {
		Vector2 position = Vector2.zero;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (stickHolder.rectTransform,
			ped.position, ped.pressEventCamera, out position)) {
			position.x = (position.x / stickHolder.rectTransform.sizeDelta.x);
			position.y = (position.y / stickHolder.rectTransform.sizeDelta.y);

			float x = (stickHolder.rectTransform.pivot.x == 1) ? position.x * 2 + 1 : position.x * 2 - 1;
			float y = (stickHolder.rectTransform.pivot.y == 1) ? position.y * 2 + 1 : position.y * 2 - 1;

			inputDirection = new Vector3 (x, 0, y);
			inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;
			analogStick.rectTransform.anchoredPosition = new Vector3 (inputDirection.x * (stickHolder.rectTransform.sizeDelta.x / 3),
				inputDirection.z * (stickHolder.rectTransform.sizeDelta.y / 3));
		}

		isDrag = true;
	}

	public virtual void OnPointerDown(PointerEventData ped) {
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped) {
		inputDirection = Vector3.zero;
		analogStick.rectTransform.anchoredPosition = Vector3.zero;
		isDrag = false;
	}
}
