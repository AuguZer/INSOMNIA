using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrawl : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Crawl;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {

    }

    public override void OnStateExit(PlayerStateManager playerState)
    {

    }
}
