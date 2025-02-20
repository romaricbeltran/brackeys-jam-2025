using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
