using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Jump;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        //TO IDLE
        if (!playerState.isJumping && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.Idle);
        }
        //TO WALK
        if (!playerState.isJumping && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Walk);
        }
        //TO RUN
        if (!playerState.isJumping && playerState.isRunning)
        {
            playerState.TransitionToState(playerState.Run);
        }
        //TO FALL
        if (playerState.isFalling)
        {
            playerState.TransitionToState(playerState.Fall);
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
