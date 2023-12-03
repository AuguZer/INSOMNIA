using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrawlIdle : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.CrawlIdle;
        playerState.inputManager.characterController.height = 0.477304f;
        Vector3 charcterControllerCenter = new Vector3(0f, -.68f, 0f);
        playerState.inputManager.characterController.center = charcterControllerCenter;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        //TO IDLE
        if (!playerState.isCrawling  && !playerState.isCrouching && playerState.inputManager.dirInput == Vector3.zero && !playerState.playerPhysics.CantGetUp())
        {
            playerState.TransitionToState(playerState.Idle);
        }

        //TO CROUCH IDLE
        if (playerState.isCrouching && !playerState.playerPhysics.CantGetUp())
        {
            playerState.TransitionToState(playerState.CrouchIdle);
        }
        //TO HIDE
        if (playerState.isHiding)
        {
            playerState.TransitionToState(playerState.Hide);
        }
        //TO CRAWL
        if (playerState.isCrawling && playerState.inputManager.dirInput != Vector3.zero)
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

    }
}
