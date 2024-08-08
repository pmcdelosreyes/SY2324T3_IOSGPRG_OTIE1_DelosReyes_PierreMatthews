using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;
    public Camera cam;
    public Vector2 adjustedPos;

    void Start()
    {
        joystickOriginalPos = cam.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / -4;
    }

    private void Update()
    {
        if (this.name == "MovementJoystick")
            joystickOriginalPos = cam.transform.position - new Vector3(5.9f, 2.9f);
        else if (this.name == "LookJoystick")
            joystickOriginalPos = cam.transform.position + new Vector3(5.9f, -2.9f);
    }

    // detect position where player is touching the screen
    public void PointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized; // get general direction where joystick is pointing

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        // limit joystick movement within background
        if (joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    // detect position where player is stops the screen
    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        joystickBG.transform.position = joystickOriginalPos;
    }
}