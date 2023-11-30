using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyBaseState
{
    public override void OnStateEnter(EnemyStateManager enemyState)
    {
        enemyState.enemyState = EnemyStateManager.EnemyState.PATROL;

    }
    public override void OnStateUpdate(EnemyStateManager enemyState)
    {
        //TO IDLE
        if (enemyState.agent.remainingDistance <= enemyState.agent.stoppingDistance)
        {
            enemyState.TransitionToState(enemyState.enemyIdle);
        }
    }
    public override void OnStateExit(EnemyStateManager enemyState)
    {
     
    }
}
