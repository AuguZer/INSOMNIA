using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventClosetDoor : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] bool playerInZone;
    [SerializeField] bool eventPlayed;

    [SerializeField] AudioClip doorKnock;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerInZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!eventPlayed)
        {
            if (playerInZone)
            {
                animator.SetTrigger("Locked");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInZone = true;
            if (!eventPlayed)
            {
                audioSource.clip = doorKnock;
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInZone = false;

            if (!eventPlayed)
            {
                audioSource.loop = false;
            }

            eventPlayed = true;
            //StopAudioClip
        }
    }
}
