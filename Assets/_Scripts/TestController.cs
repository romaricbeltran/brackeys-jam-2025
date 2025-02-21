using System;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TestController : MonoBehaviour
{
    public static Action<bool> OnInteractRequest;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float _dashStrength = 10f;

    private Vector2 m_currentDirection = Vector2.zero;
    private Rigidbody2D m_rb;
    private TrailRenderer m_trailRenderer;
    private InputBinder m_inputBinder;
    private Vector2 m_dashExtraStrength;

    void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_inputBinder = GetComponent<InputBinder>();
        m_trailRenderer = GetComponent<TrailRenderer>();
        m_trailRenderer.emitting = false;
    }

    private void OnEnable()
    {
        m_inputBinder.OnDashCooldownFinished -= OnDashAvailable;
        m_inputBinder.OnDashCooldownFinished += OnDashAvailable;
    }

    private void OnDisable()
    {
        m_inputBinder.OnDashCooldownFinished -= OnDashAvailable;

    }
    void Update()
    {
        Vector2 lastDirection = m_currentDirection;
        m_currentDirection = Vector2.zero;

        if (m_inputBinder.GetMoveLeftInput())
        {
            m_currentDirection += Vector2.left;
        }
        if (m_inputBinder.GetMoveUpInput())
        {
            m_currentDirection += Vector2.up;
        }
        if (m_inputBinder.GetMoveRightInput())
        {
            m_currentDirection += Vector2.right;
        }
        if (m_inputBinder.GetMoveDownInput())
        {
            m_currentDirection += Vector2.down;
        }

        m_currentDirection = m_currentDirection.normalized;
        // Debug.Log($"direction: {m_currentDirection}");

        bool isInteractRequested = m_inputBinder.GetInteractInput();
        OnInteractRequest?.Invoke(isInteractRequested);

        if (m_currentDirection != Vector2.zero) // Move
        {
            Broadcaster.TriggerOnAnimationRequest(transform, Utils.GetMovementAnimation(m_currentDirection));
        }
        else if (lastDirection != Vector2.zero) // Idle
        {
            Broadcaster.TriggerOnAnimationRequest(transform, Utils.GetIdleAnimation(lastDirection));
        }

        if (m_inputBinder.GetDashInput())
        {
            m_dashExtraStrength = m_currentDirection * _dashStrength;
        }

        if(m_inputBinder.GetPauseInput())
        {
            Debug.Log($"[TestController] PauseInput");
            Broadcaster.TriggerOnPauseRequest(true);
        }
    }

    void FixedUpdate()
    {
        if (m_currentDirection != Vector2.zero)
        {
            Vector2 finalForce = m_dashExtraStrength == Vector2.zero ? m_currentDirection * speed : m_dashExtraStrength;

            if (m_dashExtraStrength != Vector2.zero)
            {
                m_rb.AddForce(finalForce, ForceMode2D.Impulse);
                m_dashExtraStrength = Vector2.zero;
                Debug.Log($"DASH!!!");

                // Play sound
                Broadcaster.TriggerOnAudioRequest(AudioClipType.Dash);
                // Animate
                m_trailRenderer.emitting = true;
            }
            else
            {
                m_rb.AddForce(finalForce);
            }

        }
    }

    private void OnDashAvailable()
    {
        m_trailRenderer.emitting = false;
    }
}

public static class Utils
{
    public static AnimationType GetMovementAnimation(Vector2 direction)
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

    public static AnimationType GetIdleAnimation(Vector2 lastDirection)
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
