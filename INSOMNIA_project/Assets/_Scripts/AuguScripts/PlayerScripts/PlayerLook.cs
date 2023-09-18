using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Look;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        if (playerState.isCrouching) playerState.inputManager.speed = playerState.crouchSpeed;
        if (playerState.isCrawling) playerState.inputManager.speed = playerState.crawlSpeed;
        if (playerState.isRunning) playerState.inputManager.speed = playerState.runSpeed;

        //TO IDLE
        if (!playerState.playerCam.isLooking && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.Idle);
        }
        //TO WALK
        if (!playerState.playerCam.isLooking && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Walk);
        }
        //TO RUN
        if (!playerState.playerCam.isLooking && playerState.isRunning)
        {
            playerState.TransitionToState(playerState.Run);
        }
        //TO CRCOUCH
        if (!playerState.playerCam.isLooking && playerState.isCrouching)
        {
            playerState.TransitionToState(playerState.Crouch);
        }
        //TO CRAWL
        if (!playerState.playerCam.isLooking && playerState.isCrawling)
        {
            playerState.TransitionToState(playerState.Crawl);
        }
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {

    }
}
