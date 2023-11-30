using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyBaseState
{
    public override void OnStateEnter(EnemyStateManager enemyState)
    {
        enemyState.enemyState = EnemyStateManager.EnemyState.IDLE;
        enemyState.isInIdle = true;
        enemyState.StartCoroutine(enemyState.IdleCoroutine());
        enemyState.SetEnemyPatrolPoint();
    }
    public override void OnStateUpdate(EnemyStateManager enemyState)
    {
        //TO PATROL
        if (enemyState.agent.hasPath && !enemyState.isInIdle)
        {
            enemyState.TransitionToState(enemyState.enemyPatrol);
        }
    }
    public override void OnStateExit(EnemyStateManager enemyState)
    {

    }
}
