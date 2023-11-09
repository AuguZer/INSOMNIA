using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateManager;

public class Door : MonoBehaviour
{
    [SerializeField] bool open = false;
    [SerializeField] bool canRotate = true;
    [SerializeField] public bool openRot;
    [SerializeField] public bool closeRot;
    [SerializeField] float openAngle = -90f;
    [SerializeField] float closeAngle = 0f;
    [SerializeField] float smooth = 2f;
    [SerializeField] public float detectionRadius = 1f;

    [SerializeField] LayerMask playerMask;

    [SerializeField] GameObject doorGraphics;

    Quaternion initialRotation;

    private enum DoorState
    {
        OPEN,
        MOVE,
        CLOSE,
        STOPPED
    }

    [SerializeField] DoorState doorState;
    // Start is called before the first frame update
    void Start()
    {
        doorState = DoorState.CLOSE;
        detectionRadius = 0f;
        OnStateEnter();
    }

    public void ChangeDoorState()
    {
        if (doorState != DoorState.STOPPED)
        {
            open = !open;
        }
        else
        {
            if (!open)
            {
                TransitionToState(DoorState.CLOSE);
            }
            if (open)
            {
                TransitionToState(DoorState.OPEN);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        OnStateUpdate();
        //Debug.Log(CollisionWithPlayer());
        canRotate = CollisionWithPlayer() ? false : true;


        if (open && doorState != DoorState.STOPPED/* && canRotate*/)
        {
            closeRot = false;
            Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else if (!open && doorState != DoorState.STOPPED/*&& canRotate*/)
        {
            openRot = false;
            Quaternion targetRotation1 = Quaternion.Euler(0, closeAngle, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation1, smooth * Time.deltaTime);
        }
    }

    private bool CollisionWithPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(doorGraphics.transform.position, detectionRadius, playerMask);

        if (colliders.Length > 0)
        {
            initialRotation = transform.localRotation;
            return true;

        }
        return false;
    }

    private void OnStateEnter()
    {
        switch (doorState)
        {
            case DoorState.OPEN:
                detectionRadius = 0f;
                break;
            case DoorState.MOVE:
                StartCoroutine(OpenCoroutine());
                break;
            case DoorState.CLOSE:
                detectionRadius = 0f;
                break;
            case DoorState.STOPPED:
                transform.localRotation = initialRotation;
                detectionRadius = 0f;
                break;
            default:
                break;
        }
    }
    private void OnStateUpdate()
    {
        switch (doorState)
        {
            case DoorState.OPEN:
                //TO MOVE
                if (transform.localRotation != Quaternion.Euler(0, openAngle, 0))
                {
                    TransitionToState(DoorState.MOVE);
                }
                break;
            case DoorState.MOVE:
                //TO OPEN
                if (transform.localRotation == Quaternion.Euler(0, openAngle, 0))
                {
                    TransitionToState(DoorState.OPEN);
                }
                //TO CLOSE
                if (transform.localRotation == Quaternion.Euler(0, closeAngle, 0))
                {
                    TransitionToState(DoorState.CLOSE);
                }
                //TO STOPPED
                if (CollisionWithPlayer())
                {
                    TransitionToState(DoorState.STOPPED);
                }

                break;
            case DoorState.CLOSE:
                //TO MOVE
                if (transform.localRotation != Quaternion.Euler(0, closeAngle, 0))
                {
                    TransitionToState(DoorState.MOVE);
                }
                break;
            default:
                break;
        }
    }
    private void OnStateExit()
    {
        switch (doorState)
        {
            case DoorState.OPEN:
                break;
            case DoorState.MOVE:
                break;
            case DoorState.CLOSE:
                break;
            default:
                break;
        }
    }
    private void TransitionToState(DoorState nextState)
    {
        OnStateExit();
        doorState = nextState;
        OnStateEnter();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(doorGraphics.transform.position, detectionRadius);
    }

    IEnumerator OpenCoroutine()
    {
        yield return new WaitForSeconds(.2f);
        detectionRadius = .5f;
    }
}
