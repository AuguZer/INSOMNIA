using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCrouch : PlayerBaseState
{
    public override void OnStateEnter(PlayerStateManager playerState)
    {
        playerState.state = PlayerStateManager.PlayerState.Crouch;
        playerState.inputManager.speed = playerState.crouchSpeed;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        //TO CROUCHIDLE
        if (playerState.isCrouching && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.CrouchIdle);
        }
        //TO WALK
        if (playerState.inputManager.inputActions.FindAction("Crouch").WasPerformedThisFrame() && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Walk);
            playerState.isCrouching = false;
        }
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {
        playerState.inputManager.LerpCameraToCrouch(playerState.inputManager.cam.transform.localPosition, new Vector3(playerState.inputManager.cam.transform.localPosition.x, .61f, playerState.inputManager.cam.transform.localPosition.z), .2f);
    }
}
