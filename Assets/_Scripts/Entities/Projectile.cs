using UnityEngine;

[RequireComponent(typeof(TransformMover))]
[RequireComponent(typeof(TransformDirection))]
public class Projectile : MonoBehaviour
{
    private TransformMover m_transformMover;
    private TransformDirection m_transformDirection;

    private void Awake()
    {
        m_transformMover = GetComponent<TransformMover>();
        m_transformDirection = GetComponent<TransformDirection>();
    }

    public void Init(Transform target)
    {
        m_transformDirection.Target = target;
    }
}
