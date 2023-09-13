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
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {

    }
}
