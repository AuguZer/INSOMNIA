using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVEventObject : MonoBehaviour
{
    [SerializeField] public Transform focusPos;
    [SerializeField] public bool tvIsOff;
    // Start is called before the first frame update
    void Start()
    {
        tvIsOff = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOn()
    {
        //Play AudioClip Ringing Phone
        Debug.Log("TV is making noise");
    }

    public void TurnOff()
    {
        //Stop AudioClip
        tvIsOff = true;
        Debug.Log("TV is turnOff");
    }

}
