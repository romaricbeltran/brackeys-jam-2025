using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10);

    private Vector3 m_velocity = Vector3.zero;
    private Transform m_target;

    void FixedUpdate()
    {
        if (!IsPlayerInScene()) return; // GUARD CASE

        // Calcola la posizione desiderata della camera
        Vector3 targetPosition = m_target.position + offset;

        // Applica il damp per rendere il movimento fluido
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_velocity, smoothTime);
    }

    private bool IsPlayerInScene()
    {
        if (m_target == null)
        {
            m_target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        return m_target != null;
    }
}
