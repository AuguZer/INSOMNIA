using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTwins : MonoBehaviour
{
    [SerializeField] GameObject twinsPrefab;
    [SerializeField] GameObject destContainer;

    TwinsBehavior twinsBehavior;
    // Start is called before the first frame update
    void Start()
    {
        twinsBehavior = twinsPrefab.GetComponent<TwinsBehavior>();

        twinsPrefab.SetActive(false);
        destContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("EventTwins " + gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            twinsPrefab.SetActive(true);
            destContainer.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          this.enabled = false;
        }
    }
}
