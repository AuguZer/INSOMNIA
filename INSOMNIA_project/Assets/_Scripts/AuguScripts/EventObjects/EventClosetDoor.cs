using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventClosetDoor : MonoBehaviour
{
    [SerializeField] public Animator animator;

    [SerializeField] public bool playerInZone;
    [SerializeField] public bool eventStarted;
    [SerializeField] public bool eventPlayed;
    [SerializeField] public Transform enemyPosition;

    [SerializeField] AudioClip doorKnock;
    [SerializeField] AudioClip moodMusic;

    public AudioSource audioSource;
    [SerializeField] AudioSource moodMusicAudioSource;

    public bool isOpen;
    // Start is called before the first frame update
    public virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerInZone = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        isOpen = animator.GetBool("Open");

        if (isOpen )
        {
            audioSource.Stop();
            eventPlayed = true;
        }

        if (!eventPlayed)
        {
            if (playerInZone)
            {
                animator.SetTrigger("Locked");
            }
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInZone = true;
            eventStarted = true;
            if (!eventPlayed)
            {
                audioSource.clip = doorKnock;
                audioSource.Play();
                moodMusicAudioSource.PlayOneShot(moodMusic);
            }
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInZone = false;

            if (!eventPlayed)
            {
                audioSource.loop = false;
            }

            eventPlayed = true;
           
        }
    }
}
