using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip[] footSteps;  
    [SerializeField] AudioClip heartBeat;
    [SerializeField] public AudioClip[] hides;
    [SerializeField] public AudioClip openVentMusic;

    [SerializeField] AudioSource footStepsAudioSource;
    [SerializeField] AudioSource voicesAudioSource;
    [SerializeField] public AudioSource pickUpAudioSource;
    [SerializeField] public AudioSource hideAudioSource;
    [SerializeField] public AudioSource moodMuicAudioSource;

    [SerializeField] float heartBeatIntervale;
    [SerializeField] float breathingIntervale;

    float timer;

    PlayerStateManager playerStateManager;
    // Start is called before the first frame update
    void Start()
    {
        playerStateManager = GetComponentInParent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStateManager.isRunning)
        {
            voicesAudioSource.volume = .2f;
        }
        else
        {
            voicesAudioSource.volume = .1f;
        }


        //timer += Time.deltaTime;

        //// Si le compteur de temps a atteint l'intervalle
        //if (timer >= heartBeatIntervale)
        //{
        //    // Exécutez la fonction
        //    voicesAudioSource.clip = heartBeat;
        //    voicesAudioSource.Play();

        //    // Réinitialisez le compteur de temps
        //    timer = 0f;
        //}
        //if (timer >= breathingIntervale)
        //{
        //    // Exécutez la fonction


        //    // Réinitialisez le compteur de temps
        //    timer = 0f;
        //}
    }

    private void Steps()
    {
        if (playerStateManager.isRunning)
        {
            footStepsAudioSource.volume = .2f;
        }
        else
        {
            footStepsAudioSource.volume = .1f;
        }

        AudioClip clip = RandomStepsClip(footSteps);
        footStepsAudioSource.PlayOneShot(clip);
    }

    private AudioClip RandomStepsClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

}
