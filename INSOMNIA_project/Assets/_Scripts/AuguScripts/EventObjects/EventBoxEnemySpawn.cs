using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBoxEnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject destContainer;
    [SerializeField] GameObject doorMeetingRoom;
    AnimDoor animDoorMeetingRoom;

    [SerializeField] AudioSource moodMusicAudioSource;
    [SerializeField] AudioClip onSeeEnemyMusic;

    public Light[] lightsToSwitchOff;
    public Light[] lightsToSwitchOn;

    bool hasPlayedmusic;


    [Header("LIGHTS FLICKER")]
    public float minIntensity = 0.2f; 
    public float maxIntensity = 2f; 
    public float minFlickerInterval = 0.1f; 
    public float maxFlickerInterval = 0.5f; 
    public float steadyLightDuration;

    // Start is called before the first frame update
    void Start()
    {
        moodMusicAudioSource.clip = onSeeEnemyMusic;
        animDoorMeetingRoom = doorMeetingRoom.GetComponentInChildren<AnimDoor>();
        hasPlayedmusic = false;
        enemy.SetActive(false);
        destContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && animDoorMeetingRoom.doorOpen && !hasPlayedmusic)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                destContainer.SetActive(true);
            }

            StartCoroutine(WaitDoorOpen());
            hasPlayedmusic = true;
            this.enabled = false;
        }
    }

    public void LightsOnEvent()
    {
        foreach (Light light in lightsToSwitchOff)
        {
            light.enabled = false;
        }
        foreach (Light light in lightsToSwitchOn)
        {
            light.enabled = true;
            StartCoroutine(FlickerCycle(light));
        }
    }

    IEnumerator WaitDoorOpen()
    {
        yield return new WaitForSeconds(.6f);
        moodMusicAudioSource.Play();

    }

    IEnumerator FlickerCycle(Light light)
    {
        while (true)
        {
            // Phase de flicker
            yield return StartCoroutine(Flicker(light));

            // Phase d'éclairage constant
            light.intensity = maxIntensity;
            steadyLightDuration = Random.Range(10f, 20f);
            yield return new WaitForSeconds(steadyLightDuration);
        }
    }

    IEnumerator Flicker(Light light)
    {
        float flickerDuration = Random.Range(.5f, 1f); // Durée totale de la phase de flicker
        float startTime = Time.time;

        while (Time.time - startTime < flickerDuration)
        {
            float randomFlickerInterval = Random.Range(minFlickerInterval, maxFlickerInterval);

            // Éteindre la lumière
            light.intensity = minIntensity;
            yield return new WaitForSeconds(randomFlickerInterval);

            // Rallumer la lumière
            light.intensity = maxIntensity;
            yield return new WaitForSeconds(randomFlickerInterval);
        }
    }
}
