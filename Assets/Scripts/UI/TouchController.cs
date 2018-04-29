using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {
    public Controller controller;
    public Touch[] touches;
	// Update is called once per frame
	void Update () {
        touches = Input.touches;
        foreach (Touch touch in touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                RectTransform jouystickRectTransform = controller.joystick.GetComponent<RectTransform>();
                if (touch.position.x < jouystickRectTransform.position.x + jouystickRectTransform.rect.size.x &&
                    touch.position.x > jouystickRectTransform.position.x &&
                    touch.position.y < jouystickRectTransform.position.y + jouystickRectTransform.rect.size.y &&
                    touch.position.y > jouystickRectTransform.position.y)
                {
                    controller.joystick.Touch = touch;
                }
            }
        }

	}
}
