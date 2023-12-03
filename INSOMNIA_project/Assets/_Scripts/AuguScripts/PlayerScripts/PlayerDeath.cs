using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Death;
        //playerState.StartCoroutine(playerState.DeathCoroutine());
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {

    }

    public override void OnStateExit(PlayerStateManager playerState)
    {

    }
}
