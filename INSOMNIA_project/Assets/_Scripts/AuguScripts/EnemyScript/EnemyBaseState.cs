using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void OnStateEnter(EnemyStateManager enemyState);
    public abstract void OnStateUpdate(EnemyStateManager enemyState);
    public abstract void OnStateExit(EnemyStateManager enemyState);
}
