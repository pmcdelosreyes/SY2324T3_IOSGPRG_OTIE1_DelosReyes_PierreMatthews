using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngin.UI; // using legacy text
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance;

    public enum swipe
    {
        Left,
        Right,
        Up,
        Down
    };


    [SerializeField] TextMeshProUGUI inputInfoText;
    [SerializeField] TextMeshProUGUI hpText;

    Vector2 startPos; // helps determine swipe direction
    Vector2 endPos; // helps determine swipe direction
    Vector2 swipeDist; // helps determine if input is swipe/tap

    [SerializeField] float swipeRange; // minimum pixel requirement of swipe
    [SerializeField] float tapRange; // minimum pixel requirement of tap

    public int hp = 3;
    [SerializeField] float speed = 1;

    public Player.swipe input;

    void Update()
    {
#if UNITY_EDITOR
        UpdateMouseInput(); // for PC
#elif UNITY_ANDROID || UNITY_IOS
        UpdateTouchInput();
#endif

        UpdateHp();
    }

    // [HANDLES TOUCH INPUT]
    void UpdateTouchInput()
    {
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    startPos = Input.GetTouch(0).position;
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    endPos = Input.GetTouch(0).position;
                    swipeDist = endPos - startPos;

                    if (swipeDist.x < -swipeRange) // check if swipe left
                    {
                        UpdateText("Left");
                    }
                    else if (swipeDist.x > swipeRange) // check if swipe right
                    {
                        UpdateText("Right");
                    }
                    else if (swipeDist.y > swipeRange) // check if swipe up
                    {
                        UpdateText("Up");
                    }
                    else if (swipeDist.y < -swipeRange) // check if swipe down
                    {
                        UpdateText("Down");
                    }
                    break;

                default:
                    break;


            }
        }
    }

    // [HANDLELS MOUSE INPUT]
    void UpdateMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // if left mouse button is clicked
        {
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) // if left mouse button is pressed
        {

        }
        else if (Input.GetMouseButtonUp(0)) // if left mouse button is released
        {
            endPos = Input.mousePosition;
            swipeDist = endPos - startPos;

            // [CHECK SWIPE DIRECTION]
            if (swipeDist.x < -swipeRange) // check if swipe left
            {
                UpdateText("Left");
                input = Player.swipe.Left;
            }
            else if (swipeDist.x > swipeRange) // check if swipe right
            {
                UpdateText("Right");
                input = Player.swipe.Right;
            }
            else if (swipeDist.y > swipeRange) // check if swipe up
            {
                UpdateText("Up");
                input = Player.swipe.Up;
            }
            else if (swipeDist.y < -swipeRange) // check if swipe down
            {
                UpdateText("Down");
                input = Player.swipe.Down;
            }

            // [CHECK TAP]
            if (Mathf.Abs(swipeDist.x) < tapRange && Mathf.Abs(swipeDist.y) < tapRange) // input distance is less than tap range, then it is a tap
            {
                UpdateText("Tap");
                this.transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
        }

    }

    void UpdateText(string msg)
    {
        inputInfoText.text = "Input: " + msg;
        //hpText.text = "HP: " + hp;
    }
    void UpdateHp()
    {
        hpText.text = "HP: " + hp;
    }



}
