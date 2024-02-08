using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDoor : MonoBehaviour
{
    public bool doorOpen;
    public bool eventDoor;
    public Animator animator;

    public int keyNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        doorOpen = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Open", doorOpen);
    }

    
}
