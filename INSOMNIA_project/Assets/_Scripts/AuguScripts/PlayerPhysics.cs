using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] float detectionRadius = 2f;
    [SerializeField] LayerMask doorMask;

    [SerializeField]
    Collider[] colliders;

    [SerializeField] bool collideWithDoor;

    Grabber grabber;
    // Start is called before the first frame update
    void Start()
    {
        grabber = GetComponent<Grabber>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectDoors();
    }

    private void DetectDoors()
    {
        colliders = Physics.OverlapSphere(transform.position, detectionRadius, doorMask);

        if (colliders.Length > 0 )
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
        //if (hit.rigidbody != null)
        //{
        //    hit.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        //    hit.rigidbody.AddForce(Vector3.forward);
        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
