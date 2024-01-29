using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLand : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Land;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {

    }

    public override void OnStateExit(PlayerStateManager playerState)
    {

    }
}
