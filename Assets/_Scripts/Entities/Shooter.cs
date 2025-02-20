using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _shootingInterval = 4f;
    [SerializeField] private float _shootingRange = 10f;

    private Coroutine m_corRef;
    private bool isStop;
    private Transform m_target;
    private Transform m_player;
    private Transform m_carrier;

    private void Start()
    {
        FindStaticReferences();
        TriggerShootingCor();
    }

    private void FindStaticReferences()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null) m_player = playerGO.transform;

        GameObject carrierGO = GameObject.FindGameObjectWithTag("Carrier");
        if (carrierGO != null) m_carrier = carrierGO.transform;
    }

    public void TriggerShootingCor()
    {
        if (m_corRef == null)
        {
            m_corRef = StartCoroutine(ShootingCor());
        }
    }

    private IEnumerator ShootingCor()
    {
        while (!isStop)
        {
            yield return new WaitForSeconds(_shootingInterval);
            m_target = FindTarget();
            if (m_target && CanSeeTarget(m_target))
            {
                SpawnProjectile();
            }
        }

        m_corRef = null;
    }

    private void OnEnable()
    {
        HealthComponent.OnHealthChanged += StopShooting;
    }

    private void OnDisable()
    {
        HealthComponent.OnHealthChanged -= StopShooting;
    }

    private void StopShooting(HealthComponent healthComponent)
    {
        if (healthComponent.gameObject.CompareTag("Carrier") && healthComponent.IsDead)
        {
            isStop = true;
            m_carrier = null;
        }
    }

    private void SpawnProjectile()
    {
        if (!m_target) return;

        Projectile spawnedProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        spawnedProjectile.Init(m_target);
    }

    private Transform FindTarget()
    {
        Transform bestTarget = null;
        float bestDistance = Mathf.Infinity;

        if (m_carrier)
        {
            float carrierDist = Vector2.Distance(transform.position, m_carrier.position);
            if (carrierDist <= _shootingRange)
            {
                bestTarget = m_carrier;
                bestDistance = carrierDist;
            }
        }

        if (m_player)
        {
            float playerDist = Vector2.Distance(transform.position, m_player.position);
            if (playerDist <= _shootingRange && playerDist < bestDistance)
            {
                bestTarget = m_player;
            }
        }

        return bestTarget;
    }

    private bool CanSeeTarget(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, target.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, _obstacleMask);

        return !hit.collider || hit.collider.transform == target;
    }
}
