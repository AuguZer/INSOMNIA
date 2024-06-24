using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static PlayerStateManager;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    public EnemyIdle enemyIdle = new EnemyIdle();
    public EnemyPatrol enemyPatrol = new EnemyPatrol();
    public EnemyChase enemyChase = new EnemyChase();
    public EnemyAttack enemyAttack = new EnemyAttack();

    public enum EnemyState
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK
    }

    [Header("STATES")]
    [SerializeField] public EnemyState enemyState;

    [Header("BOOLS")]
    public bool isInIdle;
    public bool isInPatrol;
    public bool isInChase;
    public bool isInAttack;

    [Header("STATS")]
    [SerializeField] public float walkSpeed;
    [SerializeField] public float chaseSpeed;
    [SerializeField] public float attackDistance = 2.2f;

    public NavMeshAgent agent;
    [SerializeField] Transform destContainer;
    [SerializeField] List<Transform> destinations = new List<Transform>();
    public int destIndex;
    public bool hasPath;
    [SerializeField] Transform currentDest;
    Vector3 dest;

    int randomNum;

    [SerializeField] int idleTime;

    public EnemyDetection enemyDetection;
    public EnemyAnimatorManager enemyAnimatorManager;

    public bool isGoingForward;
    public bool isIdleCoroutineRunning;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyDetection = GetComponentInChildren<EnemyDetection>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        foreach (Transform destPoint in destContainer)
        {
            destinations.Add(destPoint);
        }
        if (destinations.Count > 0)
        {
            agent.SetDestination(destinations[0].position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isInIdle = true;
        currentState = enemyIdle;
        currentState.OnStateEnter(this);
        isGoingForward = true;
        destIndex = 0; // Start with the first destination
        hasPath = agent.hasPath;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInIdle)
        {
            EnemyIdle();
        }
        EnemyMove();
        hasPath = agent.hasPath;
        currentState.OnStateUpdate(this);

        //// Debugging
        //Debug.Log($"hasPath: {hasPath}, destIndex: {destIndex}, isInIdle: {isInIdle}, isIdleCoroutineRunning: {isIdleCoroutineRunning}");
    }

    private void EnemyIdle()
    {


        float distance = Vector3.Distance(agent.transform.position, destinations[destIndex].position);
        if (distance <= 0.45f && !isIdleCoroutineRunning)
        {

            StartCoroutine(IdleCoroutine());
        }
    }

    private void EnemyMove()
    {
        //// Check if agent has reached the destination
        //if (!isInIdle && !isIdleCoroutineRunning && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        //{
        //    // Ensure agent has actually stopped
        //    if (agent.isStopped)
        //    {
        //        GotoNextPoint();
        //    }
        //}

        isInChase = enemyDetection.playerDetected;
        if (isInChase)
        {
            agent.SetDestination(enemyDetection.playerPos.position);
            float distance = Vector3.Distance(transform.position, enemyDetection.playerPos.position);

            if (distance <= attackDistance && !enemyDetection.playerKilled)
            {
                isInAttack = true;
            }
        }

        if (enemyDetection.playerKilled && !isInAttack)
        {
            enemyDetection.playerDetected = false;
            isInChase = false;
            isInIdle = true;
        }
    }

    private void GotoNextPoint()
    {
        if (destinations.Count == 0)
            return;

        if (isGoingForward)
        {
            if (destIndex >= destinations.Count - 1)
            {
                isGoingForward = false;
                destIndex--;
            }
            else
            {
                destIndex++;
            }
        }
        else
        {
            if (destIndex <= 0)
            {
                isGoingForward = true;
                destIndex++;
            }
            else
            {
                destIndex--;
            }
        }

        if (destIndex >= 0 && destIndex < destinations.Count)
        {
            agent.SetDestination(destinations[destIndex].position);
        }
    }

    public IEnumerator IdleCoroutine()
    {
        Debug.Log("In coroutine");
        isInIdle = true;
        isIdleCoroutineRunning = true;
        float waitTime = Random.Range(1f, 5f);
        yield return new WaitForSeconds(waitTime);
        isInIdle = false;
        isIdleCoroutineRunning = false;
        GotoNextPoint();
    }

    public void TransitionToState(EnemyBaseState nextState)
    {
        currentState.OnStateExit(this);
        currentState = nextState;
        currentState.OnStateEnter(this);
    }
}
