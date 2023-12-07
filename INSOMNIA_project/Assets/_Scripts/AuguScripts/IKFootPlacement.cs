using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootPlacement : MonoBehaviour
{
    [SerializeField] LayerMask detectionMask;

    [Range(0f, 1f)]
    [SerializeField] float distanceToGround;

    Animator animator;
    PlayerStateManager playerStateManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerStateManager = GetComponentInParent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = new Ray();

        ray.origin = transform.position;
        ray.direction = Vector3.forward;
        Debug.DrawLine( ray.origin, ray.direction );


        if (Physics.Raycast(ray.origin, ray.direction, out hit, 2f, detectionMask))
        {
       
            //StartCoroutine(LerpPosToGround(transform.localPosition, new Vector3(transform.localPosition.x, hit.transform.localPosition.y, transform.localPosition.z), 1f));
        }


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
        Ray ray = new Ray();

        ray.origin = transform.position;
        ray.direction = Vector3.forward;
        Gizmos.DrawLine(ray.origin, ray.direction);
    }

    public IEnumerator LerpPosToGround(Vector3 startPos, Vector3 endPos, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPos;
    }
}
