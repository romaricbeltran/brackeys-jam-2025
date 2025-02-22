using UnityEngine;

[RequireComponent(typeof(TriggerHandler))]
public class TriggerEffect : MonoBehaviour
{
    private TriggerHandler m_triggerHandler;
    private Lever m_lever;
    private bool m_isTriggerState;

    void Awake()
    {
        m_triggerHandler = GetComponent<TriggerHandler>();
        m_lever = GetComponent<Lever>();
    }

    private void OnEnable()
    {
        m_triggerHandler.OnTriggerHappened -= SetTriggerState;
        m_triggerHandler.OnTriggerHappened += SetTriggerState;

        TestController.OnInteractRequest -= OnInteractRequest;
        TestController.OnInteractRequest += OnInteractRequest;
    }

    private void OnDisable()
    {
        TestController.OnInteractRequest -= OnInteractRequest;
        m_triggerHandler.OnTriggerHappened -= SetTriggerState;
    }

    private void SetTriggerState(OnTriggerPayload onTriggerPayload)
    {
        Debug.Log($"ExecuteTriggerEffect", this);
        m_isTriggerState = onTriggerPayload.IsTriggerState;
    }

    private void OnInteractRequest(bool isInteractRequested)
    {
        if (isInteractRequested && m_isTriggerState && m_lever != null)
        {
            m_lever.ToggleDoors();
        }
    }
}