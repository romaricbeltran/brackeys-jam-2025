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

    private void Start()
    {
        FindStaticTargetReference();
        TriggerShootingCor();
    }

    private void FindStaticTargetReference()
    {
        GameObject carrierGO = GameObject.FindGameObjectWithTag("Carrier");
        if (carrierGO != null) m_target = carrierGO.transform;
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
            if (m_target && CanSeeTarget(m_target))
            {
                SpawnProjectile(m_target);
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
            m_target = null;
        }
    }

    private void SpawnProjectile(Transform target)
    {
        Projectile spawnedProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        spawnedProjectile.Init(target);
    }

    private bool CanSeeTarget(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, target.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, _obstacleMask);

        return !hit.collider || hit.collider.transform == target;
    }
}
