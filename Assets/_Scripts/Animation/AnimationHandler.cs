using UnityEngine;

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

    private void SetAnimationType(AnimationType animationType)
    {
        if (m_currentAnimationType != animationType)
        {
            m_currentAnimationType = animationType;
            
            string animationTypeStr = animationType.ToString();
            m_anim.SetTrigger(animationTypeStr);
        }
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