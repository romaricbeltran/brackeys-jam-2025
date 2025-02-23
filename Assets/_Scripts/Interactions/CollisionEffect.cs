using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(HealthComponent))]
public class CollisionEffect : MonoBehaviour
{
    [SerializeField] private bool selfDestroyOnCollision;
    [SerializeField] private LayerMask _instaDeathLayerMask;
    [SerializeField] private LayerMask _damageLayerMask;
    [SerializeField] private LayerMask _gameOverLayerMask;

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

        if (selfDestroyOnCollision || IsLayerInMask(collision.gameObject.layer, _instaDeathLayerMask))
        {
            if(gameObject.CompareTag("Carrier"))
            {
                Broadcaster.TriggerGameOver(new GameOverPayLoad(false));
            }
            Destroy(gameObject);
            return;
        }

        if (IsLayerInMask(collision.gameObject.layer, _damageLayerMask))
        {
            m_health.ChangeHealth(-1);
        }

        if(IsLayerInMask(collision.gameObject.layer, _gameOverLayerMask))
        {
            Broadcaster.TriggerGameOver(new GameOverPayLoad(true));
        }
    }

    private bool IsLayerInMask(int targetLayer, LayerMask filterMask)
    {
        return ((1 << targetLayer) & filterMask) != 0;
    }
}