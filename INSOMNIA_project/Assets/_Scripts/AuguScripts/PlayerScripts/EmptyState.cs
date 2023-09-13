using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyState : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Idle;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {

    }

    public override void OnStateExit(PlayerStateManager playerState)
    {

    }
}
