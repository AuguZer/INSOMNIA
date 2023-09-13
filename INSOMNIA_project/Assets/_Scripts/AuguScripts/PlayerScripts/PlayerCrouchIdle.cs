using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdle : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.CrouchIdle;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        if (playerState.isCrouching && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Crouch);
        }
        if (playerState.inputManager.inputActions.FindAction("Crouch").WasPerformedThisFrame())
        {
            playerState.TransitionToState(playerState.Idle);
            playerState.isCrouching = false;
        }
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {
    }
}
