using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrawlIdle : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.CrawlIdle;
        playerState.inputManager.characterController.height = 1;
        playerState.inputManager.characterController.center = new Vector3(0f, -.5f, 0f);
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        //TO IDLE
        if (!playerState.isCrawling  && !playerState.isCrouching && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.Idle);
        }

        //TO CROUCH IDLE
        if (playerState.isCrouching)
        {
            playerState.TransitionToState(playerState.CrouchIdle);
        }
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {

    }
}
