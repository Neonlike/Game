using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    [Header("Fixed Joystick")]
    

    Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();

    void Start()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    //public float Horizontal()
    //{
    //    if (inputVector.x != 0) return inputVector.x;
    //    else return Input.GetAxis("Horizontal");
    //}
    //public float Vertical()
    //{
    //    if (inputVector.x != 0) return inputVector.y;
    //    else return Input.GetAxis("Vertical");
    //}
}