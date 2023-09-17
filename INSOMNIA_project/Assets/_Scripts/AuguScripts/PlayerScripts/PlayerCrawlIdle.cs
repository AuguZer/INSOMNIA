using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrawlIdle : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.CrawlIdle;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        //TO IDLE
        if (!playerState.isCrawling && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.Idle);
        }
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {

    }
}
