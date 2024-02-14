using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBoxEnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject destContainer;
    [SerializeField] GameObject doorMeetingRoom;
    // Start is called before the first frame update
    void Start()
    {
        enemy.SetActive(false);
        destContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && doorMeetingRoom.GetComponentInChildren<AnimDoor>().doorOpen)
        {
            enemy.SetActive(true);
            destContainer.SetActive(true);
        }
    }
}
