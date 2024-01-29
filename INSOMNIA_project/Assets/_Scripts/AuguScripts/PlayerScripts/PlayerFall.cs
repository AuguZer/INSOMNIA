using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Fall;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        //TO LAND
        if(!playerState.isFalling)
        {
            playerState.TransitionToState(playerState.Land);
        }
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {
        playerState.playerCam.StartCoroutine(playerState.playerCam.LerpRotationCam(playerState.playerCam.transform.localRotation, playerState.playerCam.transform.localRotation, .2f, playerState.playerCam.transform.localPosition, new Vector3(-.3f,-.5f,.3f)));
    }
}