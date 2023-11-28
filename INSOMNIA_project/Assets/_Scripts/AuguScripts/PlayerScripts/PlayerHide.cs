using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Hide;
        playerState.inputManager.characterController.enabled = false;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        //TO IDLE
        if (playerState.inputManager.dirInput == Vector3.zero && !playerState.isHiding)
        {
            playerState.TransitionToState(playerState.Idle);
        }

    }

    public override void OnStateExit(PlayerStateManager playerState)
    {
        playerState.inputManager.characterController.enabled = true;
    }
}
