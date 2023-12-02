using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyBaseState
{
    public override void OnStateEnter(EnemyStateManager enemyState)
    {
        enemyState.enemyState = EnemyStateManager.EnemyState.CHASE;
        enemyState.agent.speed = enemyState.chaseSpeed;
  
    }
    public override void OnStateUpdate(EnemyStateManager enemyState)
    {
        //TO IDLE
        if (enemyState.isInIdle && !enemyState.isInChase)
        {
            enemyState.TransitionToState(enemyState.enemyIdle);
        }
        //TO PATROL
        if (!enemyState.isInIdle && !enemyState.isInChase)
        {
            enemyState.TransitionToState(enemyState.enemyPatrol);
        }

    }
    public override void OnStateExit(EnemyStateManager enemyState)
    {

    }
}
