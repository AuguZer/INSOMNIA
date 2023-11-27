using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabber : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    [Header("Grab Settings")]
    [SerializeField] Transform grabArea;
    GameObject heldObj;
    Rigidbody heldObjRB;

    [Header("Physics Settings")]
    [SerializeField] float grabRange = 5f;
    [SerializeField] float grabForce = 150f;
    [SerializeField] float grabDrag = 10f;

    [SerializeField] bool leftButtonDown;

    PlayerInventory playerInventory;

    private void Awake()
    {
        playerInventory = GetComponentInParent<PlayerInventory>();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.FindAction("Grab").started += OnStartGrab;
        inputActions.FindAction("Grab").canceled += OnStopGrab;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.FindAction("Grab").started -= OnStartGrab;
        inputActions.FindAction("Grab").canceled -= OnStopGrab;
    }

    private void OnStartGrab(InputAction.CallbackContext ctx)
    {
        leftButtonDown = true;
    }
    private void OnStopGrab(InputAction.CallbackContext ctx)
    {
        leftButtonDown = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (leftButtonDown)
        {
            if (heldObj == null)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
                {
                    //Grab Object
                    GrabObject(hit.transform.gameObject);

                }
            }
            if (heldObj != null)
            {
                //Move Object
                MoveObject();
                if (Vector3.Distance(heldObj.transform.position, grabArea.position) > 1f)
                {
                    DropObject();
                }
            }

        }
        if (!leftButtonDown)
        {
            //Drop Object
            DropObject();
        }

        if (inputActions.FindAction("Interact").WasPerformedThisFrame())
        {
            RaycastHit hit;
           

            if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
            {
                InteractWithDoor(hit.transform.gameObject);

                InteractWithCloset(hit.transform.gameObject);
            }
        }


    }

    private void InteractWithCloset(GameObject closet)
    {
        if(closet.tag == "Closet")
        {
            if (closet.GetComponent<HideCloset>()!= null)
            {
                Transform hidePos = closet.GetComponent<HideCloset>().hidPos;
                StartCoroutine(LerpToHidePosition(transform.parent.position, new Vector3(hidePos.position.x, transform.parent.position.y, hidePos.position.z), 2f));

            }
        }
    }

    private void InteractWithDoor(GameObject door)
    {
        if (door.tag == "Door")
        {
            Animator doorAnimator = door.GetComponent<Animator>();
            AnimDoor animDoor = door.GetComponent<AnimDoor>();
            if (doorAnimator != null)
            {
                if(playerInventory.keyOwned > 0)
                {
                    animDoor.keyNumber = 1;
                    playerInventory.keyOwned = 0;
                }
                if (animDoor.keyNumber == 0)
                {
                    doorAnimator.SetTrigger("Locked");
                }
                if (animDoor.keyNumber == 1)
                {
                    if (!animDoor.doorOpen)
                    {
                        if (doorAnimator.speed != 0f)
                        {
                            animDoor.doorOpen = true;
                        }
                    }
                    else if (doorAnimator.speed != 0f)
                    {
                        animDoor.doorOpen = false;
                    }
                    doorAnimator.speed = 1f;
                }
            }
            #region
            if (door.GetComponent<Door>() != null)
            {
                door.GetComponent<Door>().ChangeDoorState();
            }
            #endregion
        }
    }

    private void GrabObject(GameObject grabObj)
    {
        if (grabObj.tag == "PickUpObj")
        {
            if (grabObj.GetComponent<Rigidbody>())
            {
                heldObjRB = grabObj.GetComponent<Rigidbody>();
                heldObjRB.useGravity = false;
                heldObjRB.drag = grabDrag;
                heldObjRB.constraints = RigidbodyConstraints.FreezeRotationZ;

                heldObjRB.transform.parent = grabArea;
                heldObj = grabObj;
            }
        }

        if(grabObj.tag == "Key")
        {
            Destroy(grabObj);
            playerInventory.keyOwned++;
        }
    }

    private void DropObject()
    {
        if (heldObjRB == null) return;

        heldObjRB.useGravity = true;
        heldObjRB.drag = 1f;
        heldObjRB.constraints = RigidbodyConstraints.None;

        heldObjRB.transform.parent = null;
        heldObj = null;
    }

    private void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, grabArea.position) > .1f)
        {
            Vector3 moveDirection = (grabArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * grabForce);
        }
    }


    public IEnumerator LerpToHidePosition(Vector3 startPos, Vector3 endPos, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            transform.parent.position = Vector3.Lerp(startPos, endPos, t / duration);
            t += Time.deltaTime;

            yield return null;
        }

        transform.parent.position = endPos;
    }
}
