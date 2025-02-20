using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(HealthComponent))]
public class CollisionEffect : MonoBehaviour
{
    [SerializeField] private bool selfDestroyOnCollision;
    [SerializeField] private LayerMask _instaDeathLayerMask;

    private HealthComponent m_health;
    private CollisionHandler m_collisionHandler;

    private void Awake()
    {
        m_health = GetComponent<HealthComponent>();
        m_collisionHandler = GetComponent<CollisionHandler>();
    }

    private void OnEnable()
    {
        m_collisionHandler.OnCollisionHappened -= ExecuteCollisionEffect;
        m_collisionHandler.OnCollisionHappened += ExecuteCollisionEffect;
    }

    private void OnDisable()
    {
        m_collisionHandler.OnCollisionHappened -= ExecuteCollisionEffect;
    }

    protected void ExecuteCollisionEffect(Collision2D collision)
    {
        // Debug.Log($"OnCollisionHappened {gameObject.name}", this);

        if (selfDestroyOnCollision && ((1 << collision.gameObject.layer) & _instaDeathLayerMask) != 0)
        {
            Destroy(gameObject);
        }

        m_health.ChangeHealth(-1);
    }
}