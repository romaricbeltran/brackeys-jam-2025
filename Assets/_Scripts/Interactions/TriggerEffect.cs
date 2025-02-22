using UnityEngine;

[RequireComponent(typeof(TriggerHandler))]
public class TriggerEffect : MonoBehaviour
{
    private TriggerHandler m_triggerHandler;
    private Lever m_lever;
    private FlowerPotion m_flowerPotion;
    private bool m_isTriggerState;

    void Awake()
    {
        m_triggerHandler = GetComponent<TriggerHandler>();
        m_lever = GetComponent<Lever>();
        m_flowerPotion = GetComponent<FlowerPotion>();
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
        
        if (isInteractRequested && m_isTriggerState && m_flowerPotion != null)
        {
            m_flowerPotion.PickUpFLower();
        }
    }
}