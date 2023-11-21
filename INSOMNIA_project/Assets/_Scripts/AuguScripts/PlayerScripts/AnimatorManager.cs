using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    PlayerInputManager playerInputManager;
    PlayerStateManager playerStateManager;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerInputManager = GetComponentInParent<PlayerInputManager>();
        playerStateManager = GetComponentInParent<PlayerStateManager>();
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region "Foward/Backward"
        if (playerInputManager.dirInput.z == 0) 
        {
            animator.SetBool("WalkFoward", false);
            animator.SetBool("WalkBackward", false);
        }
        if (playerInputManager.dirInput.z > 0)
        {
            animator.SetBool("WalkFoward", true);
            animator.SetBool("WalkBackward", false);
        }
        if (playerInputManager.dirInput.z < 0)
        {
            animator.SetBool("WalkFoward", false);
            animator.SetBool("WalkBackward", true);
        }
        #endregion

        #region "Strafe Right/Left"
        if (playerInputManager.dirInput.x == 0)
        {
            animator.speed = 1f;
            animator.SetBool("StrafeRight", false);
            animator.SetBool("StrafeLeft", false);
        }
        if (playerInputManager.dirInput.x > 0)
        {
            animator.speed = 2f;
            animator.SetBool("StrafeRight", true);
            animator.SetBool("StrafeLeft", false);
        }
        if (playerInputManager.dirInput.x < 0)
        {
            animator.speed = 2f;
            animator.SetBool("StrafeRight", false);
            animator.SetBool("StrafeLeft", true);
        }
        #endregion

        //RUN
        if (playerStateManager.state == PlayerStateManager.PlayerState.Run)
        {
            animator.speed = 1f;
            animator.SetBool("IsRunning", true);

            if (playerInputManager.dirInput.z < 0) return;

            if (playerInputManager.dirInput.x == 0)
            {
                animator.SetBool("RunStrafeRight", false);
                animator.SetBool("RunStrafeLeft", false);
            }
            if (playerInputManager.dirInput.x > 0)
            {
            
                animator.SetBool("RunStrafeRight", true);
                animator.SetBool("RunStrafeLeft", false);
            }
            if (playerInputManager.dirInput.x < 0)
            {
               
                animator.SetBool("RunStrafeRight", false);
                animator.SetBool("RunStrafeLeft", true);
            }
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("RunStrafeRight", false);
            animator.SetBool("RunStrafeLeft", false);
        }


    }
}
