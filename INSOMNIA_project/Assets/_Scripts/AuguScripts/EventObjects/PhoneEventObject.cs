using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneEventObject : MonoBehaviour
{
    [SerializeField] public Transform focusPos;
    [SerializeField] public bool phoneIsOff;
    // Start is called before the first frame update
    void Start()
    {
        phoneIsOff = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOn()
    {
        //Play AudioClip Ringing Phone
        Debug.Log("Phone is ringing");
    }

    public void TurnOff()
    {
        //Stop AudioClip
        phoneIsOff = true;
        Debug.Log("Phone stop ringing");
        Level1EventManager.instance.StartCoroutine(Level1EventManager.instance.TVTurnOnCoroutine());
    }
}
