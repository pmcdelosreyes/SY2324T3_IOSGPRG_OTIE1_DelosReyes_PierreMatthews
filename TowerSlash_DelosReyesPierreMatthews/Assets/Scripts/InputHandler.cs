using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputHandler : MonoBehaviour
{

    public DirectionType PlayerInput { get { return playerInput; } set { playerInput = value; } }

    [SerializeField] TextMeshProUGUI inputText;

    Vector2 startPos;
    Vector2 endPos;
    Vector2 swipeDistance;

    [SerializeField] float swipeRange;
    [SerializeField] float tapRange;

    DirectionType playerInput;

    void Start()
    {
        GameManager.Instance.InputHandler = this;
    }


    void Update()
    {
#if UNITY_EDITOR
        UpdateMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
        UpdateTouchInput();
#endif
    }

    void UpdateTouchInput()
    {
        if (Input.touchCount > 0)
        {
            switch(Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    startPos = Input.GetTouch(0).position;
                    break;

                case TouchPhase.Moved:

                    break;

                case TouchPhase.Ended:
                    endPos = Input.GetTouch(0).position;
                    swipeDistance = endPos - startPos;

                    if (swipeDistance.x < -swipeRange)
                    {
                        playerInput = DirectionType.Left;
                        UpdateInputText("" + playerInput);
                    }
                    else if(swipeDistance.x > swipeRange)
                    {
                        playerInput = DirectionType.Right;
                        UpdateInputText("" + playerInput);
                    }
                    else if(swipeDistance.y < -swipeRange)
                    {
                        playerInput = DirectionType.Down;
                        UpdateInputText("" + playerInput);
                    }
                    else if (swipeDistance.y > swipeRange)
                    {
                        playerInput = DirectionType.Up;
                        UpdateInputText("" + playerInput);
                    }
                    else
                    {
                        playerInput = DirectionType.None;
                        UpdateInputText("" + playerInput);
                    }

                    if (Mathf.Abs(swipeDistance.x) < tapRange && Mathf.Abs(swipeDistance.y) < tapRange)
                    {
                        UpdateInputText("tap");
                        Debug.Log("Small Dash");
                        GameManager.Instance.Score += 0.01f;

                        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                        {
                            enemy.transform.Translate(Vector2.down * 50 * Time.deltaTime);
                        }
                    }

                    break;


                default:
                    break;
            }
        }


    }

    void UpdateMouseInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {

        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            swipeDistance = endPos - startPos;

            if (swipeDistance.x < -swipeRange)
            {
                playerInput = DirectionType.Left;
                UpdateInputText("" + playerInput);

            }
            else if (swipeDistance.x > swipeRange)
            {
                playerInput = DirectionType.Right;
                UpdateInputText("" + playerInput);

            }
            else if (swipeDistance.y <- swipeRange)
            {
                playerInput = DirectionType.Down;
                UpdateInputText("" + playerInput);

            }
            else if (swipeDistance.y > swipeRange)
            {
                playerInput = DirectionType.Up;
                UpdateInputText("" + playerInput);

            }
            else
            {
                playerInput = DirectionType.None;
                UpdateInputText("" + playerInput);
            }

            if (Mathf.Abs(swipeDistance.x) < tapRange && Mathf.Abs(swipeDistance.y) < tapRange)
            {
                UpdateInputText("tap");
                Debug.Log("Small Dash");
                GameManager.Instance.Score += 0.01f;

                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.transform.Translate(Vector2.down * 50 * Time.deltaTime);
                }
            }
        }


    }

    void UpdateInputText(string msg)
    {
        inputText.text = msg;
    }
}
