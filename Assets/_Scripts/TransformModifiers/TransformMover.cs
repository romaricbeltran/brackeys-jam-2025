using UnityEngine;

public class TransformMover : MonoBehaviour
{
    [SerializeField] [Range(-0.2f, 0.2f)] float speed = .15f;

    private void FixedUpdate()
    {
        transform.position += transform.right * speed;
    }
}
