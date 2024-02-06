using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level1EventManager : MonoBehaviour
{
    [SerializeField] GameObject phone;
    [SerializeField] GameObject TV;
    [SerializeField] float timeBeforePhone;
    [SerializeField] float timeBeforeTV;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PhoneRingCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //Phase 1
    IEnumerator PhoneRingCoroutine()
    {
        yield return new WaitForSeconds(timeBeforePhone);
        phone.GetComponent<FocusObject>().TurnOn();
    }

    IEnumerator TVTurnOnCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeTV);
        TV.GetComponent<FocusObject>().TurnOn();
    }
}
