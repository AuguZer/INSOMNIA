using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrawl : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Crawl;
        playerState.inputManager.speed = playerState.crawlSpeed;
        playerState.inputManager.characterController.height = 1f;
        playerState.inputManager.characterController.center = new Vector3(0f, -.5f, 0f);
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        if (playerState.inputManager.dirInput.z <= 0f)
        {
            playerState.inputManager.speed = playerState.crawlSpeed / playerState.backwardDivider;
        }
        //TO CRAWL IDLE
        if (playerState.isCrawling && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.CrawlIdle);
        }

        //TO IDLE
        if (!playerState.isCrawling && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.Idle);
        }

        //TO WALK
        if (!playerState.isCrawling && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Walk);
        }

    }

    public override void OnStateExit(PlayerStateManager playerState)
    {

    }
}
