using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using static PlayerStateManager;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    public EnemyIdle enemyIdle = new EnemyIdle();
    public EnemyPatrol enemyPatrol = new EnemyPatrol();

    public enum EnemyState
    {
        IDLE,
        PATROL
    }

    [Header("STATES")]
    [SerializeField] public EnemyState enemyState;

    [Header("BOOLS")]
    public bool isInIdle;
    public bool isInPatrol;

    [Header("STATS")]
    [SerializeField] public float walkSpeed;

    public NavMeshAgent agent;
    [SerializeField] Transform destContainer;
    [SerializeField] List<Transform> destinations = new List<Transform>();
    public int destIndex;
    public bool hasPath;
    [SerializeField] Transform currentDest;
    Vector3 dest;

    int randomNum;

    [SerializeField] int idleTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        foreach (Transform destPoint in destContainer)
        {
            destinations.Add(destPoint);
        }
        agent.SetDestination(destinations[0].position);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        currentState = enemyPatrol;
        currentState.OnStateEnter(this);

    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate(this);

        hasPath = agent.hasPath;

    }

    private void EnemyMove()
    {
        if (!agent.hasPath)
        {
            if (destIndex == destinations.Count - 1)
            {
                destIndex = 0;  
            }
            else
            {
                destIndex++;
            }
            agent.SetDestination(destinations[destIndex].position);
        }
    }

    //public void SetEnemyPatrolPoint()
    //{
    //    randomNum = Random.Range(0, destinations.Count);
    //    currentDest = destinations[randomNum];
    //    dest = currentDest.position;
    //    agent.SetDestination(dest);
    //}
    public void TransitionToState(EnemyBaseState nextState)
    {
        currentState.OnStateExit(this);
        currentState = nextState;
        currentState.OnStateEnter(this);
    }

     public IEnumerator IdleCoroutine()
    {
        yield return new WaitForSeconds(idleTime);
        isInIdle = false;
    }
}
