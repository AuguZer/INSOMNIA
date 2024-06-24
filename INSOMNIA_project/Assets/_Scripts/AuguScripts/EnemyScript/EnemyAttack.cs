using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyBaseState
{
    public override void OnStateEnter(EnemyStateManager enemyState)
    {
        enemyState.enemyState = EnemyStateManager.EnemyState.ATTACK;
        enemyState.agent.speed = 0f;
        //enemyState.StartCoroutine(enemyState.AttackCorutine());
        enemyState.agent.SetDestination(enemyState.transform.position);
        enemyState.enemyAnimatorManager.animator.speed = 2.5f;

    }
    public override void OnStateUpdate(EnemyStateManager enemyState)
    {
        enemyState.enemyDetection.DetectPlayer();

        //TO CHASE
        if (!enemyState.isInAttack)
        {
            enemyState.TransitionToState(enemyState.enemyChase);
        }

        //TO IDLE
        if(enemyState.isInIdle && !enemyState.isInChase)
        {
            enemyState.TransitionToState(enemyState.enemyIdle);
        }

    }
    public override void OnStateExit(EnemyStateManager enemyState)
    {
        enemyState.enemyAnimatorManager.animator.speed = 1f;
        enemyState.agent.SetDestination(enemyState.enemyDetection.playerPos.position);
    }
}
