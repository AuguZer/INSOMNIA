using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    PlayerInputManager playerInputManager;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerInputManager = GetComponentInParent<PlayerInputManager>();
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInputManager.dirInput.z == 0) 
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


    }
}
