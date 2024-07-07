using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{

    [SerializeField] AudioClip[] footSteps;
    [SerializeField] AudioClip[] voices;
    [SerializeField] AudioClip scream;

    [SerializeField] AudioSource footStepsAudioSource;
    [SerializeField] AudioSource voicesAudioSource;
    [SerializeField] AudioSource screamAudioSource;

    [SerializeField] float interval;

    float timer;

    EnemyStateManager enemyStateManager;
    // Start is called before the first frame update
    void Start()
    {
        enemyStateManager = GetComponentInParent<EnemyStateManager>();  

        Voices();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Si le compteur de temps a atteint l'intervalle
        if (timer >= interval)
        {
            // Exécutez la fonction
            Voices();

            // Réinitialisez le compteur de temps
            timer = 0f;
        }

        if(!screamAudioSource.isPlaying && enemyStateManager.isInChase)
        {
            screamAudioSource.PlayOneShot(scream);
        }
    }

    private void Steps()
    {
   
        AudioClip clip = RandomStepsClip(footSteps);
        footStepsAudioSource.PlayOneShot(clip);
    }

    private void Voices()
    {
        AudioClip clip = RandomStepsClip(voices);
        voicesAudioSource.PlayOneShot(clip);
    }

    private AudioClip RandomStepsClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

}
