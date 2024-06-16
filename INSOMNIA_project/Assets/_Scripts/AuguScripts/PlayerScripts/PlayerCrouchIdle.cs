using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdle : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.CrouchIdle;
        playerState.inputManager.characterController.height = 1.169342f;
        Vector3 charcterControllerCenter = new Vector3(0f, -.33f, 0f);
        playerState.inputManager.characterController.center = charcterControllerCenter;

        playerState.cantJump = true;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        //TO CROUCH
        if (playerState.isCrouching && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Crouch);
        }

        //TO IDLE
        if (!playerState.isCrouching && playerState.inputManager.dirInput == Vector3.zero && !playerState.playerPhysics.CantGetUp())
        {
            playerState.TransitionToState(playerState.Idle);
        }
        //TO HIDE
        if (playerState.isHiding)
        {
            playerState.TransitionToState(playerState.Hide);
        }
        //TO CRAWLIDLE
        if (playerState.isCrawling)
        {
            playerState.TransitionToState(playerState.CrawlIdle);
        }
        //TO DEATH
        if (playerState.isDead)
        {
            playerState.TransitionToState(playerState.Death);
        }

    }

    public override void OnStateExit(PlayerStateManager playerState)
    {
 
    }
}
