using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] float detectionRadius = 2f;
    [SerializeField] LayerMask doorMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DetectDoors();
    }

    private void DetectDoors()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, doorMask);
        
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.GetComponent<Animator>().enabled)
            {
                collider.gameObject.GetComponent<Animator>().enabled = false;
            }
        }
      
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.rigidbody != null)
        {
            hit.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            hit.rigidbody.AddForce(Vector3.forward);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
