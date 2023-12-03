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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyDetection = GetComponentInChildren<EnemyDetection>();
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

        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            isInIdle = true;
        }
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

        isInChase = enemyDetection.playerDetected;
        if(isInChase)
        {
            agent.SetDestination(enemyDetection.playerPos.position);

            if (agent.remainingDistance <= attackDistance)
            {
                isInAttack = true;
            }
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
