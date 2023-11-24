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
        playerState.inputManager.characterController.height = 1.169342f;
        Vector3 charcterControllerCenter = new Vector3(0f, -.33f, 0f);
        playerState.inputManager.characterController.center = charcterControllerCenter;
    }
    public override void OnStateUpdate(PlayerStateManager playerState)
    {
        if (playerState.inputManager.dirInput.z <= 0f)
        {
            playerState.inputManager.speed = playerState.crouchSpeed / playerState.backwardDivider;
        }
        else
        {
            playerState.inputManager.speed = playerState.crouchSpeed;
        }
        //TO CROUCHIDLE
        if (playerState.isCrouching && playerState.inputManager.dirInput == Vector3.zero)
        {
            playerState.TransitionToState(playerState.CrouchIdle);
        }
        //TO WALK
        if (!playerState.isCrouching && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Walk);
        }
        //TO CRAWL
        if (playerState.isCrawling && playerState.inputManager.dirInput != Vector3.zero)
        {
            playerState.TransitionToState(playerState.Crawl);
        }
    }

    public override void OnStateExit(PlayerStateManager playerState)
    {
        
    }
}
