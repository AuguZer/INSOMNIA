using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneEventObject : MonoBehaviour
{
    [SerializeField] public Transform focusPos;
    [SerializeField] public bool phoneIsOff;
    [SerializeField] public bool finish;

    [SerializeField] AudioClip ringAudio;
    [SerializeField] AudioClip tunrOffAudio;
    [SerializeField] AudioClip noSignalAudio;

    [SerializeField] GameObject DoorClosetEventBox;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ringAudio;

        phoneIsOff = false;
        finish = true;

        DoorClosetEventBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOn()
    {
        finish = false;
        //Play AudioClip Ringing Phone
        audioSource.Play();
        Debug.Log("Phone is ringing");
        DoorClosetEventBox.SetActive(true);
        Debug.Log("Event door closet is Active");
    }

    public void TurnOff()
    {
        if (!finish)
        {
            //Stop AudioClip
            phoneIsOff = true;
            audioSource.Stop();
            Debug.Log("Phone stop ringing");
            StartCoroutine(StopAudio());
            Level1EventManager.instance.StartCoroutine(Level1EventManager.instance.TVTurnOnCoroutine());
            finish = true;
        }
    }

    IEnumerator StopAudio()
    {
        audioSource.volume = .1f;
        audioSource.PlayOneShot(tunrOffAudio);
        yield return new WaitForSeconds(.5f);
        audioSource.volume = .6f;
        audioSource.PlayOneShot(noSignalAudio);
        yield return new WaitForSeconds(10f);
        audioSource.Stop();
    }
}
