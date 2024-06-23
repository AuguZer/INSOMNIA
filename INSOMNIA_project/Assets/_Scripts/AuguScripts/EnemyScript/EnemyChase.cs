using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyBaseState
{
    public override void OnStateEnter(EnemyStateManager enemyState)
    {
        enemyState.enemyState = EnemyStateManager.EnemyState.CHASE;
        enemyState.agent.speed = enemyState.chaseSpeed;
        enemyState.enemyDetection.boxCollider.enabled = false;
  
    }
    public override void OnStateUpdate(EnemyStateManager enemyState)
    {
        if (enemyState.enemyDetection.DetectDoors())
        {
            enemyState.agent.speed = 0f;
        }
        if (enemyState.enemyDetection.canGo)
        {
            enemyState.agent.speed = enemyState.chaseSpeed;
        }

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
        //TO ATTACK
        if (enemyState.isInAttack)
        {
            enemyState.TransitionToState(enemyState.enemyAttack);
        }

    }
    public override void OnStateExit(EnemyStateManager enemyState)
    {
        enemyState.enemyDetection.boxCollider.enabled = true;
    }
}
