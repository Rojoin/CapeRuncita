using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private Vector2 fingerDown;
    private Vector2 fingerUp;

    public float minDistanceForSwipe = 20f;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDown = touch.position;
                fingerUp = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                fingerUp = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUp = touch.position;

                if (IsSwipeDown())
                {
                  
                }
            }
        }
    }

    bool IsSwipeDown()
    {
        return fingerDown.y - fingerUp.y > minDistanceForSwipe;
    }
}
