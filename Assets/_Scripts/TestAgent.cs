using UnityEngine;
using UnityEngine.AI;

public class TestAgent : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float distanceThreshold = 1f;

    private NavMeshAgent navMeshAgent;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = distanceThreshold;
        navMeshAgent.SetDestination(target.position);
    }
}
