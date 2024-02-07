using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    public bool ventOpen;
    Animator animator;

    public int screwDriverNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        ventOpen = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isOpen", ventOpen);
    }
}
