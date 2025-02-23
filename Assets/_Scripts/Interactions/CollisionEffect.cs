using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(HealthComponent))]
public class CollisionEffect : MonoBehaviour
{
    [SerializeField] private bool selfDestroyOnCollision;
    [SerializeField] private LayerMask _instaDeathLayerMask;
    [SerializeField] private LayerMask _damageLayerMask;
    [SerializeField] private LayerMask _gameOverLayerMask;

    [SerializeField] private TestAgent _testAgentPrefab;
    [SerializeField] private float _impulseMagnitude = 5f;

    [SerializeField] private bool _isCarrierReplicatingOnCollision = true;
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
            if (gameObject.CompareTag("Carrier"))
            {
                Broadcaster.TriggerGameOver(new GameOverPayLoad(false));
            }
            Destroy(gameObject);
            return;
        }

        if (IsLayerInMask(collision.gameObject.layer, _damageLayerMask))
        {
            if (_isCarrierReplicatingOnCollision)
            {
                if (gameObject.CompareTag("Carrier") && _testAgentPrefab != null)
                {
                    var tempGO = Instantiate(_testAgentPrefab, transform.parent);
                    tempGO.transform.position = transform.position;

                    // Access rigidbody and give a push
                    var agentRb = tempGO.GetComponent<Rigidbody2D>();
                    if (agentRb != null)
                    {
                        // Genera una direzione casuale in 2D
                        Vector2 randomDirection = Random.insideUnitCircle.normalized;

                        // Applica una forza impulsiva
                        agentRb.AddForce(randomDirection * _impulseMagnitude, ForceMode2D.Impulse);
                    }
                }
            }
            else
            {
                m_health.ChangeHealth(-1);

            }
        }

        if (IsLayerInMask(collision.gameObject.layer, _gameOverLayerMask))
        {
            Broadcaster.TriggerGameOver(new GameOverPayLoad(true));
        }
    }

    private bool IsLayerInMask(int targetLayer, LayerMask filterMask)
    {
        return ((1 << targetLayer) & filterMask) != 0;
    }
}