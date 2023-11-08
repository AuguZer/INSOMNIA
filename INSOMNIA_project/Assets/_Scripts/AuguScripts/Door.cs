using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool doorOpen;
    Animator animator;
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
