using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{

    [SerializeField] AudioClip[] footSteps;
    [SerializeField] AudioClip[] voices;

    [SerializeField] AudioSource footStepsAudioSource;
    [SerializeField] AudioSource voicesAudioSource;

    [SerializeField] float interval;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    private void Steps()
    {
        Debug.Log("step");
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
