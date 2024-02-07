using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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
    [SerializeField] public bool isHiding;
    [SerializeField] public bool isInInterationState;

    PlayerInventory playerInventory;
    PlayerStateManager playerStateManager;
    PlayerInputManager playerInputManager;
    PlayerEventsManager playerEventsManager;
    PlayerPhysics playerPhysics;
    PlayerCam playerCam;

    [SerializeField] LayerMask interactMask;

    [SerializeField] GameObject mainCam;

    private void Awake()
    {
        playerInventory = GetComponentInParent<PlayerInventory>();
        playerStateManager = GetComponentInParent<PlayerStateManager>();
        playerInputManager = GetComponentInParent<PlayerInputManager>();
        playerEventsManager = GetComponentInParent<PlayerEventsManager>();
        playerPhysics = GetComponentInParent<PlayerPhysics>();
        playerCam = GetComponent<PlayerCam>();
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

        DisplayInteractUI();

        if (inputActions.FindAction("Interact").WasPerformedThisFrame())
        {
            RaycastHit hit;


            if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
            {
                InteractWithDoor(hit.transform.gameObject);
                InteractWithHideCloset(hit.transform.gameObject);
                InteractWithHideBox(hit.transform.gameObject);
                InteractWithHideBelow(hit.transform.gameObject);
                InteractWithVent(hit.transform.gameObject);
                TakeKey(hit.transform.gameObject);
                TakeScrewDriver(hit.transform.gameObject);
                FocusOnObject(hit.transform.gameObject);
            }

            if (playerStateManager.state == PlayerStateManager.PlayerState.Hide && playerStateManager.canInteract)
            {
                playerEventsManager.EnableInteractUI?.Invoke();
                GetOutOfHide();
            }
        }


    }

    private void InteractWithHideBelow(GameObject belowObject)
    {
        if (belowObject.tag == "HideBelow")
        {
            if (belowObject.GetComponent<HideCloset>() != null)
            {
                if (playerStateManager.canInteract)
                {
                    playerStateManager.isCrawling = true;
                    playerStateManager.isCrouching = false;
                    Transform hidePos = belowObject.GetComponent<HideCloset>().hidePos;
                    playerStateManager.canInteract = false;
                    StartCoroutine(LerpToHidePosition(transform.parent.position, new Vector3(hidePos.position.x, transform.parent.position.y, hidePos.position.z), .5f));
                    StartCoroutine(LerpToHideRotation(belowObject.transform.localRotation));
                    StartCoroutine(playerInputManager.LerpCameraPosition(playerInputManager.cam.transform.localPosition, new Vector3(playerInputManager.cam.transform.localPosition.x, playerInputManager.camYposCrawl, playerCam.camZposCrawl), playerInputManager.camSpeed));
                    playerStateManager.isHiding = true;
                }
            }
        }
    }
    private void InteractWithHideBox(GameObject box)
    {
        if (box.tag == "HideBox")
        {
            if (box.GetComponent<HideCloset>() != null)
            {
                if (playerStateManager.canInteract)
                {
                    playerStateManager.isCrouching = true;
                    playerStateManager.isCrawling = false;
                    Transform hidePos = box.GetComponent<HideCloset>().hidePos;
                    playerStateManager.canInteract = false;
                    StartCoroutine(LerpToHidePosition(transform.parent.position, new Vector3(hidePos.position.x, transform.parent.position.y, hidePos.position.z), .5f));
                    StartCoroutine(LerpToHideRotation(box.transform.localRotation));
                    StartCoroutine(playerInputManager.LerpCameraPosition(playerInputManager.cam.transform.localPosition, new Vector3(playerInputManager.cam.transform.localPosition.x, playerInputManager.camYposCrouch, playerCam.camZpos), playerInputManager.camSpeed));
                    playerStateManager.isHiding = true;
                }
            }
        }
    }
    private void InteractWithHideCloset(GameObject closet)
    {
        if (closet.tag == "HideCloset")
        {
            if (closet.GetComponent<HideCloset>() != null)
            {
                if (playerStateManager.canInteract)
                {
                    playerStateManager.isCrouching = false;
                    playerStateManager.isCrawling = false;
                    playerCam.enabled = false;
                    Transform hidePos = closet.GetComponent<HideCloset>().hidePos;
                    playerStateManager.canInteract = false;
                    StartCoroutine(LerpToHidePosition(transform.parent.position, new Vector3(hidePos.position.x, transform.parent.position.y, hidePos.position.z), .5f));
                    StartCoroutine(LerpToHideRotation(closet.transform.localRotation));
                    StartCoroutine(playerInputManager.LerpCameraPosition(playerInputManager.cam.transform.localPosition, new Vector3(playerInputManager.cam.transform.localPosition.x, playerInputManager.camYposNormal, playerCam.camZpos), playerInputManager.camSpeed));
                    playerStateManager.isHiding = true;
                }
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
                if (animDoor.eventDoor)
                {
                    playerInventory.keyOwned = playerInventory.eventKeyOwned;
                }
                if (playerInventory.keyOwned > 0)
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

    private void InteractWithVent(GameObject vent)
    {
        if(vent.tag == "Vent")
        {
            Vent _vent = vent.GetComponent<Vent>();

            if(_vent.screwDriverNumber == 0)
            {
                Debug.Log("Vent is close");
            }
            if(playerInventory.screwDriver > 0)
            {
                _vent.screwDriverNumber = playerInventory.screwDriver;
            }
            if(_vent.screwDriverNumber > 0)
            {
                _vent.ventOpen = true;
            }
        }
    }

    private void TakeKey(GameObject key)
    {
        if (key.tag == "Key")
        {
            key.GetComponent<Key>().KeyCollected();
            playerInventory.eventKeyOwned++;
        }
    }
    private void TakeScrewDriver(GameObject screwDriver)
    {
        if (screwDriver.tag == "ScrewDriver")
        {
            screwDriver.GetComponent<ScrewDriver>().ScrewDriverCollected();
            playerInventory.screwDriver++;
        }
    }
    private void FocusOnObject(GameObject eventObject)
    {
        if (eventObject.tag == "Event Object")
        {
            //if (eventObject.GetComponent<PhoneEventObject>() != null)
            //{
            if (playerStateManager.canInteract)
            {
                if (!isInInterationState)
                {
                    isInInterationState = true;
                    playerStateManager.isCrouching = false;
                    playerStateManager.isCrawling = false;
                    if (eventObject.GetComponent<PhoneEventObject>() != null)
                    {
                        Transform focusPos = eventObject.GetComponent<PhoneEventObject>().focusPos;
                        playerStateManager.canInteract = false;
                        StartCoroutine(LerpToHidePosition(transform.parent.position, new Vector3(focusPos.position.x, transform.parent.position.y, focusPos.position.z), .5f));
                        StartCoroutine(LerpToHideRotation(eventObject.transform.localRotation));
                    }
                    if (eventObject.GetComponent<TVEventObject>() != null)
                    {
                        Transform focusPos = eventObject.GetComponent<TVEventObject>().focusPos;
                        playerStateManager.canInteract = false;
                        StartCoroutine(LerpToHidePosition(transform.parent.position, new Vector3(focusPos.position.x, transform.parent.position.y, focusPos.position.z), .5f));
                        StartCoroutine(LerpToHideRotation(eventObject.transform.localRotation));
                    }
                }
                else
                {
                    isInInterationState = false;
                    if (eventObject.GetComponent<PhoneEventObject>() != null)
                    {
                        eventObject.GetComponent<PhoneEventObject>().TurnOff();
                    }

                    if (eventObject.GetComponent<TVEventObject>() != null)
                    {
                        eventObject.GetComponent<TVEventObject>().TurnOff();
                    }
                }

            }
            //}
        }
    }
    private void GetOutOfHide()
    {
        if (playerPhysics.DetectHideOut())
        {
            Transform outPos = playerPhysics.hideColliders[0].GetComponent<HideCloset>().outPos;
            StartCoroutine(LerpToHidePosition(transform.parent.position, new Vector3(outPos.position.x, transform.parent.position.y, outPos.position.z), .5f));
            playerStateManager.isHiding = false;
            playerStateManager.canInteract = false;
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

        if (grabObj.tag == "Key")
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

    private void DisplayInteractUI()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange, interactMask))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
            if (!isHiding)
                playerStateManager.canInteract = true;
        }
        else if (playerStateManager.state != PlayerStateManager.PlayerState.Hide)
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            playerStateManager.canInteract = false;
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
        playerStateManager.canInteract = true;
        playerCam.enabled = true;
    }

    private IEnumerator LerpToHideRotation(Quaternion endRot)
    {
        float t = 0f;
        float duration = .2f;

        while (t < duration)
        {
            isHiding = true;
            transform.parent.localRotation = Quaternion.Lerp(transform.parent.localRotation, endRot, t / duration);
            t += Time.deltaTime;
            //playerStateManager.playerCam.enabled = false;
            yield return null;
        }

        isHiding = false;
        transform.parent.localRotation = endRot;
        //playerStateManager.playerCam.enabled = true;
    }
}
