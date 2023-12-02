using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyBaseState
{
    public override void OnStateEnter(EnemyStateManager enemyState)
    {
        enemyState.enemyState = EnemyStateManager.EnemyState.ATTACK;
        enemyState.agent.speed = 0f;

    }
    public override void OnStateUpdate(EnemyStateManager enemyState)
    {
        if (enemyState.enemyDetection.DetectPlayer())
        {
            enemyState.isInIdle = true;
            enemyState.isInAttack = false;
            enemyState.isInChase = false;
        }
      

        //TO CHASE
        if (!enemyState.isInAttack)
        {
            enemyState.TransitionToState(enemyState.enemyChase);
        }

    }
    public override void OnStateExit(EnemyStateManager enemyState)
    {

    }
}
