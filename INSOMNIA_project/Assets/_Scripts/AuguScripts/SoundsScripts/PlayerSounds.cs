using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip[] footSteps;
    [SerializeField] AudioClip heartBeat;

    [SerializeField] AudioSource footStepsAudioSource;
    [SerializeField] AudioSource voicesAudioSource;

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
        if(playerStateManager.isRunning)
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
        Debug.Log("step");
        AudioClip clip = RandomStepsClip(footSteps);
        footStepsAudioSource.PlayOneShot(clip);
    }

    private AudioClip RandomStepsClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

}
