using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;

    void Start()
    {
        InvokeRepeating("SpawnProjectile", 1f, 1f);
    }
    
    private void SpawnProjectile()
    {
        Projectile spawnedProjectile = Instantiate(_projectilePrefab, GameObject.FindGameObjectWithTag("World").transform, true);
        spawnedProjectile.transform.position = transform.position;
        spawnedProjectile.Init(GameObject.FindGameObjectWithTag("Carrier").transform);
    }
}