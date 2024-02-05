using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [SerializeField] bool startPhone;
    [SerializeField] float timeBeforePhone;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PhoneRingCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator PhoneRingCoroutine()
    {
        yield return new WaitForSeconds(timeBeforePhone);
        Debug.Log("Phone is ringing");
    }
}
