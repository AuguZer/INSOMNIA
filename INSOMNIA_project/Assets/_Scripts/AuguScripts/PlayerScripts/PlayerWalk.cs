using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Walk;
        playerState.inputManager.speed = playerState.walkSpeed;
        playerState.inputManager.characterController.height = 1.6f;
        Vector3 charcterControllerCenter = new Vector3(0f, -.15f, 0f);
        playerState.inputManager.characterController.center = charcterControllerCenter;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        if (playerState.inputManager.dirInput.z <= 0f)
        {
            playerState.inputManager.speed = playerState.walkSpeed / playerState.backwardDivider;
        }
        else
        {
            playerState.inputManager.speed = playerState.walkSpeed;
        }
        //TO IDLE
        if (playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.Idle);
        }
        //TO RUN
        if (playerState.isRunning)
        {
            playerState.TransitionToState(playerState.Run);
        }
        //TO CROUCH
        if (playerState.isCrouching)
        {
            playerState.TransitionToState(playerState.Crouch);
        }
        //TO CRAWL
        if (playerState.isCrawling)
        {
            playerState.TransitionToState(playerState.Crawl);
        }
        //TO JUMP
        if (playerState.isJumping)
        {
            playerState.TransitionToState(playerState.Jump);
        }
        //TO FALL
        if (playerState.isFalling)
        {
            playerState.TransitionToState(playerState.Fall);
        }
        //TO HIDE
        if (playerState.isHiding)
        {
            playerState.TransitionToState(playerState.Hide);
        }
        //TO DEATH
        if (playerState.isDead)
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
