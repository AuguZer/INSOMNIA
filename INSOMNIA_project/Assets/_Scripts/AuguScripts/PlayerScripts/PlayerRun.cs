using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Run;
        playerState.inputManager.speed = playerState.runSpeed;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        //TO IDLE
        if (!playerState.isRunning && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.Idle);
        }
        //TO WALK
        if (!playerState.isRunning && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Walk);
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
