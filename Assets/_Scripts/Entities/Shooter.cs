using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _shootingInterval = 4f;

    private Coroutine m_corRef;
    private bool isStop;
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
            SpawnProjectile();
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
        Projectile spawnedProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);

        Transform target = m_carrier ?? m_player;
        if (target != null)
        {
            spawnedProjectile.Init(target);
        }
    }
}