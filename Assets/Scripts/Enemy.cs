using UnityEngine;

public class Enemy : MonoBehaviour, IResettable
{
    private void OnEnable() => ResettableRegistry.Register(this);
    private void OnDisable() => ResettableRegistry.Unregister(this);

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    public void ResetState()
    {
        transform.position = startPosition;
    }
}