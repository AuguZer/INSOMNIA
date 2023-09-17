using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Idle;
        playerState.inputManager.characterController.height = 2;
        playerState.inputManager.characterController.center = Vector3.zero;

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
        //TO LOOK
        if (playerState.playerCam.isLooking)
        {
            playerState.TransitionToState(playerState.Look);
        }
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {

    }
}
