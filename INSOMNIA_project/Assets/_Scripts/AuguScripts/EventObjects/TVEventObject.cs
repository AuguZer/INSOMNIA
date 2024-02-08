using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TVEventObject : MonoBehaviour
{
    [SerializeField] public Transform focusPos;
    [SerializeField] public bool tvIsOff;
    [SerializeField] public bool finish;

    public event Action OnTVTurnOff;
    // Start is called before the first frame update
    void Start()
    {
        OnTVTurnOff += CloseMeetingRoomDoor;
        OnTVTurnOff += LockerRoomAccess;
        tvIsOff = false;
        finish = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOn()
    {
        finish = false;
        //Play AudioClip Ringing Phone
        Debug.Log("TV is making noise");
    }

    public void TurnOff()
    {
        if (!finish)
        {
            //Stop AudioClip
            tvIsOff = true;
            Debug.Log("TV is turnOff");
            OnTVTurnOff?.Invoke();
            finish = true;
        }
    }


    public void CloseMeetingRoomDoor()
    {
        if (Level1EventManager.instance.doorMeetingRoom.GetComponent<AnimDoor>().doorOpen)
        {
            Level1EventManager.instance.doorMeetingRoom.GetComponent<AnimDoor>().doorOpen = false;
        }
        Debug.Log("Door Close with loud noise");
        //Play Noise AudioClip
    }

    public void LockerRoomAccess()
    {
        Level1EventManager.instance.doorLockerRoom.GetComponent<AnimDoor>().keyNumber = 1;
        Level1EventManager.instance.doorLockerRoom.GetComponent<AnimDoor>().doorOpen = true;
        Level1EventManager.instance.eventBoxEnemySpawn.SetActive(true);
        Debug.Log("Lockeroom Door open, lights switch off + only light in locker Room");

        //SwitchOff Every Lights Except in Locker Room
    }
}
