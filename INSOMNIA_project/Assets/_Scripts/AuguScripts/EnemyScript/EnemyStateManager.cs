using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    // Start is called before the first frame update
    void Start()
    {
        currentState = enemyIdle;
        currentState.OnStateEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate(this);
    }

    public void TransitionToState(EnemyBaseState nextState)
    {
        currentState.OnStateExit(this);
        currentState = nextState;
        currentState.OnStateEnter(this);
    }
}
