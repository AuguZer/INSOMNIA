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
        if(enemyState.isInIdle)
        {
            enemyState.TransitionToState(enemyState.enemyIdle);
        }
        //TO CHASE
        if(enemyState.isInChase)
        {
            enemyState.TransitionToState(enemyState.enemyChase);
        }
    }
    public override void OnStateExit(EnemyStateManager enemyState)
    {
     
    }
}
