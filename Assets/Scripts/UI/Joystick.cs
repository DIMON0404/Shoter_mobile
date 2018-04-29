using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public Controller controller;
    public RectTransform joystick;
    private bool isJoystickPressed = false;
    private RectTransform rectTransform;
    private Vector2 joysticjStartPosition;
    private Vector2 sizeOfRect;
    private float angel = 0;
    public Touch Touch
    {
        get
        {
            return _touch;
        }
        set
        {
            print("Set touch");
            _touch = value;
            fingerIdOfTouch = value.fingerId;
            JoystickDown();
        }
    }
    private Touch _touch;
    private int fingerIdOfTouch;

    [Header("For debug")]
    public bool __mouseRead = false;

    public Vector2 JoystickValue
    {
        get { return _joystickjValue; }
    }
    private Vector2 _joystickjValue = new Vector2(0, 0);

    public void OnPointerDown(PointerEventData eventData)
    {
#if UNITY_EDITOR
        JoystickDown();
#endif
    }

    public void OnPointerUp(PointerEventData eventData)
    {
#if UNITY_EDITOR
        JoystickUp();
#endif
    }


    private void JoystickDown()
    {
        print("Down press");
        isJoystickPressed = true;
    }

    private void JoystickUp()
    {
        print("Up press");
        isJoystickPressed = false;
        joystick.localPosition = joysticjStartPosition;
        _joystickjValue = new Vector2(0, 0);
    }
    // Use this for initialization
    void Start () {
        rectTransform = GetComponent<RectTransform>();
        sizeOfRect = new Vector2(GetComponent<RectTransform>().rect.size.x, GetComponent<RectTransform>().rect.size.y);
        joysticjStartPosition = joystick.localPosition;
    }
	
	// Update is called once per frame
	void Update () {

        if (isJoystickPressed)
        {
            foreach (Touch touch in controller.touchController.touches)
            {
                if (touch.fingerId == fingerIdOfTouch)
                {
                    _touch = touch;
                }
            }
            print(_touch.phase);

            if (_touch.phase == TouchPhase.Ended)
            {
                JoystickUp();
            }
            else
            {
                joystick.position = _touch.position;
#if UNITY_EDITOR
                if (__mouseRead)
                {
                    joystick.position = Input.mousePosition;
                }
#endif
                
                angel = Mathf.Atan2(joystick.localPosition.x - joysticjStartPosition.x, joystick.localPosition.y - joysticjStartPosition.y);
                if (Mathf.Sin(angel) >= 0)
                {
                    if (joystick.localPosition.x > joysticjStartPosition.x + Mathf.Sin(angel) * sizeOfRect.x / 2)
                    {
                        joystick.localPosition = new Vector2(joysticjStartPosition.x + Mathf.Sin(angel) * sizeOfRect.x / 2, joystick.localPosition.y);
                    }
                }
                else
                {
                    if (joystick.localPosition.x < joysticjStartPosition.x + Mathf.Sin(angel) * sizeOfRect.x / 2)
                    {
                        joystick.localPosition = new Vector2(joysticjStartPosition.x + Mathf.Sin(angel) * sizeOfRect.x / 2, joystick.localPosition.y);
                    }
                }
                if (Mathf.Cos(angel) >= 0)
                {
                    if (joystick.localPosition.y > joysticjStartPosition.y + Mathf.Cos(angel) * sizeOfRect.y / 2)
                    {
                        joystick.localPosition = new Vector2(joystick.localPosition.x, joysticjStartPosition.y + Mathf.Cos(angel) * sizeOfRect.y / 2);
                    }
                }
                else
                {
                    if (joystick.localPosition.y < joysticjStartPosition.y + Mathf.Cos(angel) * sizeOfRect.y / 2)
                    {
                        joystick.localPosition = new Vector2(joystick.localPosition.x, joysticjStartPosition.y + Mathf.Cos(angel) * sizeOfRect.y / 2);
                    }
                }
                _joystickjValue = new Vector2(
                    Mathf.RoundToInt(((joystick.localPosition.x - joysticjStartPosition.x) * 2 / sizeOfRect.x) * 10) / 10f,
                    Mathf.RoundToInt(((joystick.localPosition.y - joysticjStartPosition.y) * 2 / sizeOfRect.y) * 10) / 10f
                    );
                controller.PlayerController.MoveAndRotatePlayer(JoystickValue);
            }
        }
	}
}
