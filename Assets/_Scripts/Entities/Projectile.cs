using UnityEngine;

[RequireComponent(typeof(TransformDirection))]
public class Projectile : MonoBehaviour
{
    private TransformDirection m_transformDirection;

    private void Awake()
    {
        m_transformDirection = GetComponent<TransformDirection>();
    }

    private void Start()
    {
        Broadcaster.TriggerOnAudioRequest(AudioClipType.Projectile);
    }

    public void Init(Transform target)
    {
        m_transformDirection.Target = target;
    }
}
