using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Idle;
        playerState.inputManager.characterController.height = 1.6f;
        Vector3 charcterControllerCenter = new Vector3(0f, -.15f, 0f);
        playerState.inputManager.characterController.center = charcterControllerCenter;

    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        //TO WALK
        if (playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Walk);
        }
        //TO RUN
        if (playerState.isRunning && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Run);
        }
        //TO CROUCHIDLE
        if (playerState.isCrouching)
        {
            playerState.TransitionToState(playerState.CrouchIdle);
        }
        //TO CRAWLIDLE
        if (playerState.isCrawling)
        {
            playerState.TransitionToState(playerState.CrawlIdle);
        }
        //TO JUMP
        if(playerState.isJumping)
        {
            playerState.TransitionToState(playerState.Jump);
        }
        //TO HIDE
        if(playerState.isHiding)
        {
            playerState.TransitionToState(playerState.Hide);
        }

        //TO DEATH
        if(playerState.isDead)
        {
            playerState.TransitionToState(playerState.Death);
        }

        ////TO LOOK
        //if (playerState.playerCam.isLooking)
        //{
        //    playerState.TransitionToState(playerState.Look);
        //}
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {
        
    }
}
