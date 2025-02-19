using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _shootingInterval = 4f;

    void Start()
    {
        InvokeRepeating("SpawnProjectile", _shootingInterval, _shootingInterval);
    }
    
    private void SpawnProjectile()
    {
        Projectile spawnedProjectile = Instantiate(_projectilePrefab, GameObject.FindGameObjectWithTag("World").transform, true);
        spawnedProjectile.transform.position = transform.position;
        spawnedProjectile.Init(GameObject.FindGameObjectWithTag("Carrier").transform);
    }
}