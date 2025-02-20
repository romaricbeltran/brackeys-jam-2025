// using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class TestAgent : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float distanceThreshold = 1f;

    private Vector2 _currentDirection;

    private NavMeshAgent navMeshAgent;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }


    void Update()
    {
        // In Update for faster testing
        
        Vector2 lastDirection = _currentDirection;
        _currentDirection = new Vector2(navMeshAgent.velocity.x, navMeshAgent.velocity.y);
       
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = distanceThreshold;
        navMeshAgent.SetDestination(target.position);

        if (_currentDirection != Vector2.zero) // Move
        {
            Broadcaster.TriggerOnAnimationRequest(transform, Utils.GetMovementAnimation(_currentDirection));
        }
        else if (lastDirection != Vector2.zero) // Idle
        {
            Broadcaster.TriggerOnAnimationRequest(transform, Utils.GetIdleAnimation(lastDirection));
        }
        // Debug.Log($"Velocity : {navMeshAgent.velocity}\ndesiredVelocity : {navMeshAgent.desiredVelocity}");
    }
}
