using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // Il GameObject da seguire
    [SerializeField] private float smoothTime = 0.3f; // Tempo di damping
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); // Offset della camera

    private Vector3 velocity = Vector3.zero; // Velocità per il damping

    void FixedUpdate()
    {
        if (target == null) return;

        // Calcola la posizione desiderata della camera
        Vector3 targetPosition = target.position + offset;

        // Applica il damp per rendere il movimento fluido
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
