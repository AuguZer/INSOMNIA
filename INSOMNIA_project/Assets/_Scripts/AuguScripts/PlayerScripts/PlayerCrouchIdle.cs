using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdle : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.CrouchIdle;
        playerState.inputManager.characterController.height = 1;
        playerState.inputManager.characterController.center = new Vector3(0f, -.5f, 0f);
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        //TO CROUCH
        if (playerState.isCrouching && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Crouch);
        }

        //TO IDLE
        if (!playerState.isCrouching && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.Idle);
        }
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {
 
    }
}
