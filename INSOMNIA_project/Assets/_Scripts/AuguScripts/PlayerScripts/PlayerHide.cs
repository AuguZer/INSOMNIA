using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Hide;
        //playerState.inputManager.characterController.enabled = false;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        if (playerState.canInteract)
        {
            playerState.playerCam.CameraOnFocus();
        }
        //TO IDLE
        if (playerState.inputManager.dirInput == Vector3.zero && !playerState.isHiding)
        {
            playerState.TransitionToState(playerState.Idle);
        }
        //TO WALK
        if (playerState.inputManager.dirInput != Vector3.zero && !playerState.isHiding)
        {
            playerState.TransitionToState(playerState.Walk);
        }
        //TO CROUCH
        if (playerState.isCrouching && !playerState.isHiding)
        {
            playerState.TransitionToState(playerState.Crouch);
        }
        //TO CRAWL
        if (playerState.isCrawling && !playerState.isHiding)
        {
            playerState.TransitionToState(playerState.Crawl);
        }
        //TO DEATH
        if (playerState.isDead)
        {
            playerState.TransitionToState(playerState.Death);
        }

    }

    public override void OnStateExit(PlayerStateManager playerState)
    {
        //playerState.inputManager.characterController.enabled = true;
    }
}
