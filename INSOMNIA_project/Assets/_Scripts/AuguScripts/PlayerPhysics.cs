using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [Header("BODY PHYSIC DETECTION")]
    [SerializeField] float detectionRadius = 2f;
    [SerializeField] LayerMask doorMask;
    [SerializeField]
    Collider[] colliders;

    [Header("GROUND DETECTION")]
    [SerializeField] float groundDetectionRadius = 1f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    [Header("UP DETECTION")]
    [SerializeField] float upDetectionRadius = 1f;
    [SerializeField] LayerMask upMask;
    [SerializeField] float headDetecitionRadius = 1f;
    [SerializeField] Transform headTransform;

    [SerializeField] CharacterController characterController;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DetectDoors();
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        if (characterController.enabled)
        {
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    public bool HeadDetection()
    {
        return Physics.OverlapSphere(headTransform.position, upDetectionRadius, upMask).Length > 0;
    }
    public bool CantGetUp()
    {
        Vector3 ccBoundsMax = characterController.bounds.max;
        Vector3 newPos = new Vector3(transform.position.x, ccBoundsMax.y, transform.position.z);
        return Physics.OverlapSphere(newPos, upDetectionRadius, upMask).Length > 0;
    }
    private bool IsGrounded()
    {
        Vector3 ccBoundsMin = characterController.bounds.min;
        Vector3 newPos = new Vector3(transform.position.x, ccBoundsMin.y, transform.position.z);
        return Physics.OverlapSphere(newPos, groundDetectionRadius, groundMask).Length > 0;
    }
    private void DetectDoors()
    {
        colliders = Physics.OverlapSphere(transform.position, detectionRadius, doorMask);

        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                Animator animator = collider.GetComponent<Animator>();
                if (animator != null)
                {
                    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                    if (stateInfo.IsName("OpenDoor") || stateInfo.IsName("CloseDoor"))
                    {
                        animator.speed = 0f;
                    }
                }
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log(hit.gameObject.name);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 ccBoundsMin = characterController.bounds.min;
        Vector3 ccBoundsMax = characterController.bounds.max;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.green;
        Vector3 newPos = new Vector3(transform.position.x, ccBoundsMin.y, transform.position.z);
        Gizmos.DrawWireSphere(newPos, groundDetectionRadius);

        Gizmos.color = Color.yellow;
        Vector3 upPos = new Vector3(transform.position.x, ccBoundsMax.y, transform.position.z);
        Gizmos.DrawWireSphere(upPos, upDetectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(headTransform.position, headDetecitionRadius);
    }
}
