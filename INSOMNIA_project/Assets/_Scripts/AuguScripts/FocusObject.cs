using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusObject : MonoBehaviour
{
    [SerializeField] public Transform focusPos;
    [SerializeField] public bool objectIsOff;
    // Start is called before the first frame update
    void Start()
    {
        objectIsOff = false;
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
        objectIsOff = true;
        Debug.Log("Phone stop ringing");
    }
}
