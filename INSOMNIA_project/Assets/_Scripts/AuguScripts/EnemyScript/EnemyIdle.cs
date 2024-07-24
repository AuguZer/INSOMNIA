using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyBaseState
{
    public override void OnStateEnter(EnemyStateManager enemyState)
    {
        enemyState.enemyState = EnemyStateManager.EnemyState.IDLE;
        enemyState.agent.speed = 0;
        //enemyState.StartCoroutine(enemyState.IdleCoroutine());
    }
    public override void OnStateUpdate(EnemyStateManager enemyState)
    {
        if (enemyState.uiManager.gamePaused)
        {
            enemyState.agent.speed = 0f;
            enemyState.enemyAnimatorManager.animator.speed = 0f;
        }
        else
        {
            enemyState.agent.speed = 0f;
            enemyState.enemyAnimatorManager.animator.speed = 1f;
        }

        //TO PATROL
        if (!enemyState.isInIdle)
        {
            enemyState.TransitionToState(enemyState.enemyPatrol);
        }
        //TO CHASE
        if (enemyState.isInChase)
        {
            enemyState.TransitionToState(enemyState.enemyChase);
        }
    }
    public override void OnStateExit(EnemyStateManager enemyState)
    {

    }
}
