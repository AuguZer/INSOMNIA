using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyBaseState
{
    public override void OnStateEnter(EnemyStateManager enemyState)
    {
        enemyState.enemyState = EnemyStateManager.EnemyState.IDLE;
    }
    public override void OnStateUpdate(EnemyStateManager enemyState)
    {

    }
    public override void OnStateExit(EnemyStateManager enemyState)
    {

    }
}
