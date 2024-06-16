using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLand : PlayerBaseState
{

    Vector3 cameralook;
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Land;
        playerState.inputManager.speed = 0f;
        playerState.StartCoroutine(playerState.LandCoroutine());
        playerState.StartCoroutine(playerState.GetupFromLand());
        //playerState.playerCam.maxLookUp = 60f; 
     
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        if (playerState.getUpFromLanding)
        {
            //playerState.playerCam.sensX = 0;
            //playerState.playerCam.sensY = 0;
            playerState.playerCam.StartCoroutine(playerState.playerCam.LerpRotationCam(playerState.playerCam.transform.localRotation, Quaternion.Euler(playerState.playerCam.xRotation, 0f, 0f), .9f, playerState.playerCam.transform.localPosition, new Vector3(0f, .6f, .25f)));
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
        //playerState.playerCam.maxLookUp = -80f;
        //playerState.playerCam.sensX = 500;
        //playerState.playerCam.sensY = 500;
    }
}

