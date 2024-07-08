using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTwins : MonoBehaviour
{
    [SerializeField] GameObject twinsPrefab;
    [SerializeField] GameObject destContainer;

    [SerializeField] PhoneEventObject phone;
    TwinsBehavior twinsBehavior;

    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
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
            if (phone != null)
            {
                StartCoroutine(Level1EventManager.instance.PhoneRingCoroutine());
            }

            twinsPrefab.SetActive(true);
            destContainer.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            boxCollider.enabled = false;
        }
    }
}
