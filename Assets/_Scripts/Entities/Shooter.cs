using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _shootingInterval = 4f;

    private Coroutine m_corRef;

    private bool isStop;

    private void Start()
    {
        TriggerShootingCor();
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

        isStop =false;
        m_corRef = null;
    }

    private void OnEnable()
    {
        HealthComponent.OnHealthChanged -= StopShooting;
        HealthComponent.OnHealthChanged += StopShooting;
    }

    private void OnDisable()
    {
        HealthComponent.OnHealthChanged -= StopShooting;
    }

    private void StopShooting(HealthComponent healthComponent)
    {
        if (healthComponent.gameObject.tag == "Carrier" && healthComponent.IsDead)
        {
            isStop = true;
        }
    }

    private void SpawnProjectile()
    {
        Projectile spawnedProjectile = Instantiate(_projectilePrefab, GameObject.FindGameObjectWithTag("World").transform, true);
        spawnedProjectile.transform.position = transform.position;
        spawnedProjectile.Init(GameObject.FindGameObjectWithTag("Carrier").transform);
    }
}