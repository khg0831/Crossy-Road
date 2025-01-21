using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float startTouchTime;
    private Vector2 stratTouchPos;
    private float endTouchTime;
    private float maxSwipeTime = 2f;
    private int currentTouchCnt;
    private float PressTime = 1f;

    public bool IsTouch { get; private set; }
    public bool Tab { get; private set; }
    public bool Press { get; private set; }
    public Vector2 Swipe { get; private set; }

    private Touch touch;

    public float minSwipeDistance = 0.25f;
    public float minSwipeDistancePixels;

    public void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchTime = Time.time;
                    stratTouchPos = touch.position;
                    if (Input.touchCount == 1)
                        currentTouchCnt++;
                    else
                        currentTouchCnt = 0;
                    IsTouch = true;
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (Time.time - startTouchTime > PressTime)
                        Press = true;
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    var direction = stratTouchPos - touch.position;
                    var distance = direction.magnitude;

                    if (Time.time - startTouchTime < maxSwipeTime &&
                        distance > minSwipeDistancePixels)
                    {
                        Swipe = direction.normalized;
                    }
                    endTouchTime = Time.time;
                    break;
            }

        }
    }
}

