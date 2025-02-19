using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(HealthComponent))]
public class CollisionEffect : MonoBehaviour
{
    private HealthComponent m_health;
    private CollisionHandler m_collisionHandler;

    private void Awake()
    {
        m_health = GetComponent<HealthComponent>();
        m_collisionHandler = GetComponent<CollisionHandler>();
    }

    private void OnEnable()
    {
        m_collisionHandler.OnCollisionHappened -= OnCollisionHappened;
        m_collisionHandler.OnCollisionHappened += OnCollisionHappened;
    }

    private void OnDisable()
    {
        m_collisionHandler.OnCollisionHappened -= OnCollisionHappened;
    }

    protected void OnCollisionHappened(Collision2D collision)
    {
        Debug.Log($"OnCollisionHappened {gameObject.name}", this);
        m_health.ChangeHealth(-10);
    }
}
