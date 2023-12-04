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
    [SerializeField] public Transform attackPoint;
    [SerializeField] public float attackRadius = 1f;

    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask playerMask;

    [SerializeField] bool wallDetected;

    [SerializeField] public List<GameObject> gameObjects;

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
                wallDetected = true;
                if (!gameObjects.Contains(hit.transform.gameObject))
                {
                    gameObjects.Add(hit.transform.gameObject);
                }
            }
            if (Physics.Raycast(ray.origin, ray.direction, out hit, detectionRayLenght, playerMask))
            {
                Debug.DrawLine(headPoint.position, hit.point, Color.green);
                if (!gameObjects.Contains(hit.transform.gameObject))
                {
                    gameObjects.Add(hit.transform.gameObject);
                }
            }

            if(gameObjects.Count > 0)
            {
                if (gameObjects[0].tag == "Player")
                {
                    playerDetected = true;
                }
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
    }
}
