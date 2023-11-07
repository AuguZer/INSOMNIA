using System.Collections;
using System.Collections.Generic;
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
            }

        }


    }

    private void InteractWithDoor(GameObject door)
    {
        if (door.gameObject.tag == "Door")
        {
            if (!door.GetComponent<Door>().doorOpen)
            {
                door.GetComponent<Door>().doorOpen = true;
            }
            else
            {
                door.GetComponent<Door>().doorOpen = false;
            }
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

}
