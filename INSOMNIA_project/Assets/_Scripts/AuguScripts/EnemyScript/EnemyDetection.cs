using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public bool playerDetected;
    public Transform playerPos;

    public Vector3 playerLastPosition;

    [SerializeField] float timeBeforeEndChase;
    [SerializeField] Transform headPoint;

    [SerializeField] LayerMask wallMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            RaycastHit hit;

            //if (Physics.Raycast(headPoint.position, playerPos.position, out hit, 4f))
            //{

            //}

            Ray ray = new Ray();

            ray.origin = headPoint.position;
            ray.direction = playerPos.position - headPoint.position;

            if (Physics.Raycast(ray.origin, ray.direction,out hit, 10f, wallMask))
            {
                    Debug.DrawLine(headPoint.position, hit.point, Color.red);
            }
            else
            {
                Debug.DrawLine(headPoint.position, playerPos.position, Color.green);
            }


        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerPos = other.gameObject.transform;
            StopAllCoroutines();
            playerDetected = true;
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
        Vector3 lastPos = playerPos.position;
        playerLastPosition = lastPos;
        Debug.Log(playerLastPosition);
        playerDetected = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (playerDetected)
        {
            Gizmos.color = Color.red;
            Ray r = new Ray();

            r.origin = headPoint.position;
            r.direction = playerPos.position - headPoint.position;

            Gizmos.DrawRay(r.origin, r.direction);
        }
    }
}
