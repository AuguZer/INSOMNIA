using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool open = false;
    [SerializeField] bool canRotate = true;
    [SerializeField] bool openRot;
    [SerializeField] bool closeRot;
    [SerializeField] float openAngle = -90f;
    [SerializeField] float closeAngle = 0f;
    [SerializeField] float smooth = 2f;
    [SerializeField] float detectionRadius = 1f;

    [SerializeField] LayerMask playerMask;

    [SerializeField] GameObject doorGraphics;

    Quaternion initialRotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeDoorState()
    {
        open = !open;
    }
    // Update is called once per frame
    void Update()
    {
        //CollisionWithPlayer();
        Debug.Log(CollisionWithPlayer());
        canRotate = CollisionWithPlayer() ? false : true;


        if (open && canRotate)
        {
            closeRot = false;
            Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else if (!open && canRotate)
        {
            openRot = false;
            Quaternion targetRotation1 = Quaternion.Euler(0, closeAngle, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation1, smooth * Time.deltaTime);
        }

        if (transform.localRotation == Quaternion.Euler(0, openAngle, 0))
        {
            openRot = true;
            closeRot = false;
        }
        if (transform.localRotation == Quaternion.Euler(0, closeAngle, 0))
        {
            openRot = false;
            closeRot = true;
        }

    }

    private bool CollisionWithPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(doorGraphics.transform.position, detectionRadius, playerMask);

        if (colliders.Length > 0)
        {
            if (!openRot && !closeRot)
            {
                initialRotation = transform.localRotation;
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(doorGraphics.transform.position, detectionRadius);
    }
}
