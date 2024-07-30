using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Blinds : MonoBehaviour
{
    [SerializeField] AudioClip openBlind;
    [SerializeField] AudioClip closeBlind;

    public bool isOpen;

    Animator animator;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isOpen", isOpen);
    }

    public void OpenSound()
    {
        audioSource.PlayOneShot(openBlind);
    }

    public void CloseSound()
    {
        audioSource.PlayOneShot(closeBlind);
    }
}
