using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [Header("BODY PHYSIC DETECTION")]
    [SerializeField] float detectionRadius = 2f;
    [SerializeField] LayerMask doorMask;
    [SerializeField] LayerMask hideOutMask;
    Collider[] doorColliders;
    public Collider[] hideColliders;

    [Header("GROUND DETECTION")]
    [SerializeField] float groundDetectionRadius = 1f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] public float timeOnAir;
    Vector3 velocity;

    [SerializeField] Vector3 endPosition;


    [Header("UP DETECTION")]
    [SerializeField] float upDetectionRadius = 1f;
    [SerializeField] LayerMask upMask;
    [SerializeField] float headDetecitionRadius = 1f;
    [SerializeField] Transform headTransform;

    [SerializeField] CharacterController characterController;

    PlayerStateManager playerStateManager;
    PlayerInputManager playerInputManager;
    PlayerCam playerCam;
    Grabber grabber;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        playerStateManager = GetComponent<PlayerStateManager>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerCam = GetComponentInChildren<PlayerCam>();
        grabber = GetComponentInChildren<Grabber>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectDoors();
        DetectHideOut();


        if (playerInputManager.inputActions.FindAction("Jump").WasPerformedThisFrame() && IsGrounded())
        {
            Debug.Log("Jump");
            velocity.y = jumpHeight;
            playerStateManager.isJumping = true;
        }
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
            timeOnAir += Time.deltaTime;
        }
   
        if (characterController.enabled)
        {
            characterController.Move(velocity * Time.deltaTime);
        }

        if (!IsGrounded() && !playerStateManager.isJumping)
        {
            playerStateManager.isFalling = true;
        }
        else
        {
            playerStateManager.isFalling = false;
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
    public bool IsGrounded()
    {
        timeOnAir = 0f;
        Vector3 ccBoundsMin = characterController.bounds.min;
        Vector3 newPos = new Vector3(transform.position.x, ccBoundsMin.y, transform.position.z);
        return Physics.OverlapSphere(newPos, groundDetectionRadius, groundMask).Length > 0;
    }
    private void DetectDoors()
    {
        doorColliders = Physics.OverlapSphere(transform.position, detectionRadius, doorMask);

        if (doorColliders.Length > 0)
        {
            foreach (Collider collider in doorColliders)
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
    public bool DetectHideOut()
    {
        if (playerStateManager.isHiding)
        {
            hideColliders = Physics.OverlapSphere(transform.position, detectionRadius, hideOutMask);
            if (hideColliders.Length > 0)
            {
                return true;
            }
        }
        return false;
    }

    public void Land()
    {
        //if(!playerStateManager.isFalling)
        //{
        //    playerStateManager.isLanding = true;
        //    playerCam.StartCoroutine(playerCam.LerpRotationCam(Quaternion.identity, Quaternion.identity, .1f, playerCam.transform.localPosition, endPosition));
        //}
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
