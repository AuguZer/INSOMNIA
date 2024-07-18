using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TwinsBehavior : MonoBehaviour
{

    public Transform[] destinations;
    public int currentDestinationIndex;
    private NavMeshAgent navMeshAgent;

    [SerializeField] LayerMask playerMask;
    [SerializeField] float detectionRadius;

    TwinsAnimatorManager twinsAnimatorManager;
    
    [SerializeField] EventTwins eventTwins;

    public UnityEvent PlayerWins;

    void Start()
    {
        twinsAnimatorManager = GetComponentInChildren<TwinsAnimatorManager>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        currentDestinationIndex = 0;
        SetDestination();
    }

    void SetDestination()
    {
        if (destinations.Length == 0)
        {
            Debug.LogWarning("Aucun point de destination défini.");
            return;
        }

        navMeshAgent.SetDestination(destinations[currentDestinationIndex].position);
    }

    void Update()
    {
        twinsAnimatorManager.isRunning = DetectPlayer();
        // Vérifier si l'agent a atteint sa destination actuelle
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            // Changer de destination
            currentDestinationIndex = (currentDestinationIndex + 1) % destinations.Length;
            SetDestination();
        }
    }

    public bool DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, playerMask);

        foreach (Collider collider in colliders)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerWins?.Invoke();
        }
    }
}

