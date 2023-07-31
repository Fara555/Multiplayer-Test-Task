using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler //interfaces  for joystick inputs
{
    [Header("JoyStick parts: ")]
    [SerializeField] private RectTransform backgroundJoystick, handleJoyStick;

    [HideInInspector] public Vector3 joystickVector;
   
    private float handleRange = 1f;

    public void OnPointerUp(PointerEventData eventData) // set joustick vector to zero if user dont touch joystick
    {
        joystickVector = Vector3.zero;
        handleJoyStick.anchoredPosition = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData) //call OnDrag event if user hold joystick 
    {
        OnDrag(eventData);
    }

     public void OnDrag(PointerEventData eventData) //calcualte current joystick vector
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundJoystick, eventData.position, eventData.pressEventCamera, out Vector2 localPoint)) //event of current joystick handle position
        {
            var sizeBackgroundJoystick = backgroundJoystick.sizeDelta;
            localPoint /= sizeBackgroundJoystick;

            joystickVector = new Vector3(localPoint.x * 2 - 1, localPoint.y * 2 - 1, 0);
            joystickVector = joystickVector.magnitude > 1f ? joystickVector.normalized : joystickVector;

            float handlePos = sizeBackgroundJoystick.x / 2 * handleRange;
            handleJoyStick.anchoredPosition = new Vector2(joystickVector.x * handlePos, joystickVector.y * handlePos);
        }
    }
}
