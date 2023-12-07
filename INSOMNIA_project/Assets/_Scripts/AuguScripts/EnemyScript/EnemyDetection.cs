using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] public Transform attackPoint;
    [SerializeField] public float attackRadius = 1f;
    [SerializeField] public float detectionRadius = 1f;

    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask doorMask;

    [SerializeField] bool wallDetected;
    [SerializeField] public bool canGo;

    EnemyStateManager enemyStateManager;
    // Start is called before the first frame update
    void Start()
    {
        attackRadius = 0f;
        enemyStateManager = GetComponentInParent<EnemyStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStateManager.isInChase)
        {
            DetectDoors();
        }
        if (playerInZone)
        {
            Debug.Log(RayDetectWall());
            Debug.Log(RayDetectPlayer());

            if(RayDetectPlayer() && !RayDetectWall())
            {
                playerDetected = true;
            }
        }
    }

    public bool RayDetectWall()
    {
        RaycastHit hit;
        Ray ray = new Ray();
        ray.origin = headPoint.position;
        ray.direction = playerPos.position - headPoint.position;

        float distance = Vector3.Distance(transform.position, playerPos.position);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, distance, wallMask))
        {
            Debug.DrawLine(headPoint.position, hit.point, Color.red);
            return true;
        }

        return false;
    }

    public bool RayDetectPlayer()
    {
        RaycastHit hit;
        Ray ray = new Ray();
        ray.origin = headPoint.position;
        ray.direction = playerPos.position - headPoint.position;

        float distance = Vector3.Distance(transform.position, playerPos.position);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, distance, playerMask))
        {
            Debug.DrawLine(headPoint.position, hit.point, Color.green);
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            playerPos = other.gameObject.transform;
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
    public bool DetectPlayer()
    {
        Collider[] player = Physics.OverlapSphere(attackPoint.position, attackRadius, playerMask);

        foreach (Collider col in player)
        {
            col.gameObject.GetComponent<PlayerStateManager>().isDead = true;
            if (col.gameObject.GetComponent<PlayerStateManager>().isDead)
            {
                playerDetected = false;
                playerInZone = false;
            }
            return true;
        }
        return false;
    }

    public bool DetectDoors()
    {
        Collider[] doors = Physics.OverlapSphere(transform.position, detectionRadius, doorMask);

        foreach (Collider door in doors)
        {
            AnimDoor animDoor = door.GetComponent<AnimDoor>();
            if (animDoor != null)
            {
                animDoor.doorOpen = true;
            }
            Animator anim = door.GetComponent<Animator>();
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("WAIT OPEN"))
            {
                canGo = true;
            }
            return true;
        }
        return false;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
