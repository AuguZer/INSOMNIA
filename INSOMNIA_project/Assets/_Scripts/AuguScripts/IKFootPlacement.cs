using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootPlacement : MonoBehaviour
{
    [SerializeField] LayerMask detectionMask;

    [Range(0f, 1f)]
    [SerializeField] float distanceToGround;

    Vector3 startPos;
    Vector3 rayOrigin;

    Animator animator;
    PlayerStateManager playerStateManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerStateManager = GetComponentInParent<PlayerStateManager>();

        startPos = transform.localPosition;
        rayOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStateManager.state == PlayerStateManager.PlayerState.Idle)
        {
            RaycastHit hit;
            Ray ray = new Ray();

            ray.origin = transform.position;
            ray.direction = Vector3.down;
            Debug.DrawLine(ray.origin, ray.direction);


            if (Physics.Raycast(ray.origin, ray.direction, out hit, 2f, detectionMask))
            {
                Debug.Log("Rayon touche le des escaliers");
                Debug.Log(hit.point.y);
                StartCoroutine(LerpPosToGround(transform.position, new Vector3(transform.position.x, hit.point.y, transform.position.z), 0f));
            }
        }
        //else
        //{
        //    transform.localPosition = startPos;
        //    transform.localRotation = Quaternion.identity;
        //}


    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            //Left Foot
            RaycastHit hit;
            Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, distanceToGround + 2f, detectionMask))
            {

                if (hit.collider.gameObject.layer == 15)
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }

            }

            //Right Foot
            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, distanceToGround + 2f, detectionMask))
            {

                if (hit.collider.gameObject.layer == 15)
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.white;
        //Ray ray = new Ray();

        //ray.origin = transform.position;
        //ray.direction = Vector3.down;

        //Gizmos.DrawRay(ray);
    }

    public IEnumerator LerpPosToGround(Vector3 startPos, Vector3 endPos, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
    }
}
