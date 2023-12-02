using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public bool playerInZone;
    public bool playerDetected;
    public Transform playerPos;

    public Vector3 playerLastPosition;

    [SerializeField] float timeBeforeEndChase;
    [SerializeField] float detectionRayLenght = 50f;
    [SerializeField] Transform headPoint;

    [SerializeField] LayerMask wallMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInZone)
        {
            RaycastHit hit;
            Ray ray = new Ray();
            ray.origin = headPoint.position;
            ray.direction = playerPos.position - headPoint.position;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, detectionRayLenght, wallMask))
            {
                Debug.DrawLine(headPoint.position, hit.point, Color.red);
                playerDetected = false;
            }
            else
            {
                Debug.DrawLine(headPoint.position, playerPos.position, Color.green);
                playerDetected = true;

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerPos = other.gameObject.transform;
            StopAllCoroutines();
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(StopChaseCoroutine());
        }
    }

    IEnumerator StopChaseCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeEndChase);
        //Vector3 lastPos = playerPos.position;
        //playerLastPosition = lastPos;
        //Debug.Log(playerLastPosition);
        playerDetected = false;
        playerInZone = false;
    }
}
