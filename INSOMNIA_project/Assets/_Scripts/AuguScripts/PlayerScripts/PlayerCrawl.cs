using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrawl : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Crawl;
        playerState.inputManager.speed = playerState.crawlSpeed;
        playerState.inputManager.characterController.height = 0.477304f;
        Vector3 charcterControllerCenter = new Vector3(0f, -.68f, 0f);
        playerState.inputManager.characterController.center = charcterControllerCenter;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        if (playerState.inputManager.dirInput.z <= 0f)
        {
            playerState.inputManager.speed = playerState.crawlSpeed / playerState.backwardDivider;
        }
        else
        {
            playerState.inputManager.speed = playerState.crawlSpeed;
        }
        //TO CRAWL IDLE
        if (playerState.isCrawling && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.CrawlIdle);
        }

        //TO IDLE
        if (!playerState.isCrawling && playerState.inputManager.dirInput == Vector3.zero && !playerState.playerPhysics.CantGetUp())
        {
            playerState.TransitionToState(playerState.Idle);
        }
        //TO HIDE
        if (playerState.isHiding)
        {
            playerState.TransitionToState(playerState.Hide);
        }
        //TO WALK
        if (!playerState.isCrawling && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Walk);
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
