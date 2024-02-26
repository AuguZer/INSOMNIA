using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventClosetDoor : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] bool playerInZone;
    [SerializeField] bool eventPlayed;
    // Start is called before the first frame update
    void Start()
    {
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
            //PlayAudioClip
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInZone = false;
            eventPlayed = true;
            //StopAudioClip
        }
    }
}
