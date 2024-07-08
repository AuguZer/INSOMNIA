using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{

    [SerializeField] AudioClip[] footSteps;
    [SerializeField] AudioClip[] voices;
    [SerializeField] AudioClip scream;
    [SerializeField] AudioClip[] attacks;
    [SerializeField] AudioClip onChaseStartMusic;
    [SerializeField] AudioClip onChaseLoopMusic;
    [SerializeField] AudioClip onChaseEndMusic;

    [SerializeField] AudioSource footStepsAudioSource;
    [SerializeField] AudioSource voicesAudioSource;
    [SerializeField] AudioSource screamAudioSource;
    [SerializeField] AudioSource attackAudioSource;
    [SerializeField] AudioSource moodMusicAudioSource;

    [SerializeField] float interval;

    float timer;

    EnemyStateManager enemyStateManager;

    public bool startAudioHasPlayed = false;
    public bool loopAudioHasPlayed = false;
    public bool endAudioHasPlayed = false;
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

        if (!screamAudioSource.isPlaying && enemyStateManager.isInChase)
        {
            Debug.Log("play audio scream");
            screamAudioSource.clip = scream;
            screamAudioSource.PlayOneShot(scream);
        }
        if (!moodMusicAudioSource.isPlaying && enemyStateManager.isInChase)
        {
            if (!startAudioHasPlayed)
            {
                moodMusicAudioSource.PlayOneShot(onChaseStartMusic);
                startAudioHasPlayed = true;
            }

            if (startAudioHasPlayed && !loopAudioHasPlayed)
            {
                moodMusicAudioSource.clip = onChaseLoopMusic;
                moodMusicAudioSource.loop = true;
                moodMusicAudioSource.Play();
                loopAudioHasPlayed = true;
            }


        }

        if (startAudioHasPlayed && loopAudioHasPlayed && !enemyStateManager.isInChase && !endAudioHasPlayed)
        {
            moodMusicAudioSource.loop = false;
        }

        if (!moodMusicAudioSource.isPlaying)
        {
            if (startAudioHasPlayed && loopAudioHasPlayed && !enemyStateManager.isInChase && !endAudioHasPlayed)
            {
                moodMusicAudioSource.clip = onChaseEndMusic;
                moodMusicAudioSource.Play();
                endAudioHasPlayed = true;
            }
        }

        if (endAudioHasPlayed && !enemyStateManager.isInChase)
        {
            startAudioHasPlayed = false;
            loopAudioHasPlayed = false;
            endAudioHasPlayed = false;
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

    private void Attacks()
    {
        AudioClip clip = RandomStepsClip(attacks);
        attackAudioSource.PlayOneShot(clip);
    }

    private AudioClip RandomStepsClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

}
