using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdle : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.CrouchIdle;
        playerState.inputManager.characterController.height = 1.2f;
        Vector3 charcterControllerCenter = new Vector3(0f, -.4f, 0f);
        playerState.inputManager.characterController.center = charcterControllerCenter;
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

        //TO CRAWLIDLE
        if (playerState.isCrawling)
        {
            playerState.TransitionToState(playerState.CrawlIdle);
        }
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {
 
    }
}
