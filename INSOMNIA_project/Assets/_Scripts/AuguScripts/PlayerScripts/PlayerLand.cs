using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLand : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Land;
        playerState.StartCoroutine(playerState.LandCoroutine());
        playerState.StartCoroutine(playerState.GetupFromLand());
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        if (playerState.getUpFromLanding)
        {
            playerState.playerCam.StartCoroutine(playerState.playerCam.LerpRotationCam(playerState.playerCam.transform.localRotation, playerState.playerCam.transform.localRotation, .6f, playerState.playerCam.transform.localPosition, new Vector3(0f, .6f, .25f)));
        }
        //TO IDLE
        if (!playerState.isLanding && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.Idle);
        }
        //TO WALK
        if (!playerState.isLanding && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Walk);
        }
        //TO RUN
        if (!playerState.isLanding && playerState.isRunning && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Run);
        }

    }

    public override void OnStateExit(PlayerStateManager playerState)
    {
       playerState.getUpFromLanding = false;
    }
}

