using UnityEngine;
using UnityEngine.AI;

public class TestAgent : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distanceThreshold = 1f;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.stoppingDistance = distanceThreshold;
        navMeshAgent.SetDestination(target.position);
        // if ((target.position - transform.position).magnitude >= distanceThreshold)
        // {
        //     navMeshAgent.SetDestination(target.position);
        // }
        // else
        // {
        //     navMeshAgent.stop
        // }
    }
}
