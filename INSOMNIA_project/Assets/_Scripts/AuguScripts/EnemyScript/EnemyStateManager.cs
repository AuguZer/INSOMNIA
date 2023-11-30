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

    NavMeshPath navMeshPath;

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
    public List<Transform> destinations;
    [SerializeField] Transform currentDest;
    Vector3 dest;

    int randomNum;

    [SerializeField] int idleTime;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = enemyPatrol;
        currentState.OnStateEnter(this);

        agent.SetDestination(destinations[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate(this);

    }


    public void SetEnemyPatrolPoint()
    {
        randomNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randomNum];
        dest = currentDest.position;
        agent.SetDestination(dest);
    }
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
