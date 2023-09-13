using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Walk;
        playerState.inputManager.speed = playerState.walkSpeed;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
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
