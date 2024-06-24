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

    bool isGoingForward;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyDetection = GetComponentInChildren<EnemyDetection>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        foreach (Transform destPoint in destContainer)
        {
            destinations.Add(destPoint);
        }
        agent.SetDestination(destinations[0].position);
    }
    // Start is called before the first frame update
    void Start()
    {
        isInIdle = true;
        currentState = enemyIdle;
        currentState.OnStateEnter(this);
        isGoingForward = true;

    }

    // Update is called once per frame
    void Update()
    {
        EnemyIdle();
        EnemyMove();
        hasPath = agent.hasPath;
        currentState.OnStateUpdate(this);


    }

    private void EnemyIdle()
    {

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isInIdle = true;
        }
    }

    private void EnemyMove()
    {
        if (!agent.hasPath)
        {
            if (isGoingForward)
            {
                if (destIndex == destinations.Count - 1)
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
                if (destIndex == 0)
                {
                    isGoingForward = true;
                    destIndex++;
                }
                else
                {
                    destIndex--;
                }
            }
            agent.SetDestination(destinations[destIndex].position);
        }

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
        Debug.Log("coco");
        int randomTime = Random.Range(1, 5);
        idleTime = randomTime;
        yield return new WaitForSeconds(idleTime);
        isInIdle = false;
    }
}
