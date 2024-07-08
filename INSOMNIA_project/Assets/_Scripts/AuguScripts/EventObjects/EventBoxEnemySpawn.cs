using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBoxEnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject destContainer;
    [SerializeField] GameObject doorMeetingRoom;

    [SerializeField] AudioSource moodMusicAudioSource;
    [SerializeField] AudioClip onSeeEnemyMusic;

    public Light[] lightsToSwitchOff;
    public Light[] lightsToSwitchOn;

    bool hasPlayedmusic;
    // Start is called before the first frame update
    void Start()
    {
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
        if (other.gameObject.tag == "Player" && doorMeetingRoom.GetComponentInChildren<AnimDoor>().doorOpen && !hasPlayedmusic)
        {
            enemy.SetActive(true);
            destContainer.SetActive(true);

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
        }
    }

    IEnumerator WaitDoorOpen()
    {
        yield return new WaitForSeconds(.5f);
        moodMusicAudioSource.PlayOneShot(onSeeEnemyMusic);
       
    }
}
