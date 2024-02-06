using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level1EventManager : MonoBehaviour
{
    public static Level1EventManager instance;

    [SerializeField] GameObject phone;
    [SerializeField] GameObject TV;
    [SerializeField] float timeBeforePhone;
    [SerializeField] float timeBeforeTV;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
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
        phone.GetComponent<PhoneEventObject>().TurnOn();
    }

    public IEnumerator TVTurnOnCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeTV);
        TV.GetComponent<TVEventObject>().TurnOn();
    }
}
