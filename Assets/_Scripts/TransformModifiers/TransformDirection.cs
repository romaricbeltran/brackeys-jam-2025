using UnityEngine;

public class TransformDirection : MonoBehaviour
{
    private Transform _target;

    public Transform Target
    {
        get { return _target;}
        set { _target = value;}
    }

    private void Start()
    {
        float angle = Mathf.Atan2(Target.position.y - transform.position.y, Target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
    }
}
