using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class is receiveing all animation request.
/// All requests are filtered:
/// By type to avoid retriggering the same animation more then once
/// By sender so only broadcasters from the same transform can modify animations.
/// </summary>
[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    private Animator m_anim;
    private AnimationType m_currentAnimationType;


    private void Awake()
    {
        m_anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Broadcaster.OnPlayerAnimationRequest -= SetAnimationType;
        Broadcaster.OnPlayerAnimationRequest += SetAnimationType;
    }

    private void OnDisable()
    {
        Broadcaster.OnPlayerAnimationRequest -= SetAnimationType;

    }

    private void Start()
    {
        m_anim.SetTrigger("IdleDown");
    }

    private void SetAnimationType(Transform sender, AnimationType animationType)
    {
        if (m_currentAnimationType == animationType) return; // GUARD CASE
        if (sender != transform) return; // GUARD CASE

        m_currentAnimationType = animationType;
        string animationTypeStr = animationType.ToString();
        // Debug.Log($"I received this animation request {gameObject.name}", this);
        m_anim.SetTrigger(animationTypeStr);
    }
}

public enum AnimationType
{
    None,
    MoveUp,
    MoveLeft,
    MoveDown,
    MoveRight,
    IdleUp,
    IdleLeft,
    IdleDown,
    IdleRight,
}