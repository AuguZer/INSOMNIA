using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask playerMask;
    [SerializeField] bool playerInZone;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnBecameInvisible()
    {
        if (playerInZone)
        {
            Debug.Log("It's invisible");
            transform.LookAt(player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInZone = false;
        }
    }

}
