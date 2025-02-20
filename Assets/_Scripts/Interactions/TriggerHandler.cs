using System;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;

    public Action<OnTriggerPayload> OnTriggerHappened;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log($"TriggerStay - {gameObject.name} with {collision.gameObject.name}", this);
        OnTriggerHappened?.Invoke(new OnTriggerPayload(true, collision));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"OnTriggerExit - {gameObject.name}", this);
        OnTriggerHappened?.Invoke(new OnTriggerPayload(false, collision));
    }

}

public class OnTriggerPayload
{
    public bool IsTriggerState;
    public Collider2D Collision;

    public OnTriggerPayload(bool isTriggerState, Collider2D collision)
    {
        IsTriggerState = isTriggerState;
        Collision = collision;
    }
}