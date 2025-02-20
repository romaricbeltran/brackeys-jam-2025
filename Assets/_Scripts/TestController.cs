using System;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public static Action<bool> OnInteractRequest;

    [SerializeField] private float speed = 5.0f;

    private Vector2 currentDirection = Vector2.zero;
    private Rigidbody2D rb;
    private InputBinder inputBinder;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputBinder = GetComponent<InputBinder>();
    }

    void Update()
    {
        Vector2 lastDirection = currentDirection;
        currentDirection = Vector2.zero;

        if(inputBinder.GetMoveLeftInput())
        {
            currentDirection += Vector2.left;
        }
        if(inputBinder.GetMoveUpInput())
        {
            currentDirection += Vector2.up;
        }
        if(inputBinder.GetMoveRightInput())
        {
            currentDirection += Vector2.right;
        }
        if(inputBinder.GetMoveDownInput())
        {
            currentDirection += Vector2.down;
        }

        currentDirection = currentDirection.normalized;
        // Debug.Log($"direction: {direction}");

        bool isInteractRequested = inputBinder.GetInteractInput();
        OnInteractRequest?.Invoke(isInteractRequested);

        if(currentDirection != Vector2.zero) // Move
        {
            Broadcaster.TriggerOnAnimationRequest(GetMovementAnimation(currentDirection));
        }
        else if(lastDirection != Vector2.zero) // Idle
        {
            Broadcaster.TriggerOnAnimationRequest(GetIdleAnimation(lastDirection));
        }
    }

    void FixedUpdate()
    {
        if (currentDirection != Vector2.zero)
        {
            rb.AddForce(currentDirection * speed);
        }
    }

    private AnimationType GetMovementAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x > 0 ? AnimationType.MoveRight : AnimationType.MoveLeft;
        }
        else
        {
            return direction.y > 0 ? AnimationType.MoveUp : AnimationType.MoveDown;
        }
    }
   
    private AnimationType GetIdleAnimation(Vector2 lastDirection)
    {
        if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
        {
            return lastDirection.x > 0 ? AnimationType.IdleRight : AnimationType.IdleLeft;
        }
        else
        {
            return lastDirection.y > 0 ? AnimationType.IdleUp : AnimationType.IdleDown;
        }
    }
}
