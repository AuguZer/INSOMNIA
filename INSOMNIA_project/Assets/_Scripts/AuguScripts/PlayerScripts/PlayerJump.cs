using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Jump;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
       
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {

    }
}
