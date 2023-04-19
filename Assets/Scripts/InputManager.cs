using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;

    [SerializeField] private float minDistanceForSwipe = 20f;
    [SerializeField] private PlayerController player;

    private void Update()
    {
        if (player.isDead) return;

        if (Input.touchCount != 1) return;
        var touch = Input.GetTouch(0);

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
                player.SlideMovement();
            else
                player.JumpMovement();
        }
    }

    private bool IsSwipeDown()
    {
        return fingerDown.y - fingerUp.y > minDistanceForSwipe;
    }
}