using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [SerializeField] GameObject mainCam;

    [Header("Focus Objects Cam Look")]
    [Header("PHONE")]
    [SerializeField] float phoneMaxDown;
    [SerializeField] float phoneMaxUp;
    [Space(10)]
    [Header("TV")]
    [SerializeField] float TVMaxDown;
    [SerializeField] float TVMaxUp;


    [Header("UI")]
    [SerializeField] Sprite noPickUpImage;
    [SerializeField] Sprite pickUpImage;
    [SerializeField] Image interactUIImage;
    [SerializeField] LayerMask interactMask;
    [SerializeField] LayerMask nonInteractMask;

    PlayerInventory playerInventory;
    PlayerStateManager playerStateManager;
    PlayerInputManager playerInputManager;
    PlayerEventsManager playerEventsManager;
    PlayerPhysics playerPhysics;
    PlayerCam playerCam;
    [SerializeField] PlayerSounds playerSounds;

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
                InteractWithBlinds(hit.transform.gameObject);
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


    private void InteractWithBlinds(GameObject blindObject)
    {
        if (blindObject.tag == "Blinds")
        {
            Debug.Log("izi");
            Blinds blindsCript = blindObject.GetComponentInParent<Blinds>();
            if (blindObject.GetComponentInParent<Blinds>() != null)
            {

                if (!blindsCript.isOpen)
                {
                    Debug.Log("open it");
                    blindsCript.isOpen = true;
                }
                else
                {
                    Debug.Log("close it");
                    blindsCript.isOpen = false;
                }

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
                    playerSounds.hideAudioSource.PlayOneShot(playerSounds.hides[0]);
                    playerStateManager.isCrawling = true;
                    playerStateManager.isCrouching = false;
                    Transform hidePos = belowObject.GetComponent<HideCloset>().hidePos;
                    playerStateManager.canInteract = false;
                    StartCoroutine(LerpToWantedPosition(transform.parent.position, new Vector3(hidePos.position.x, transform.parent.position.y, hidePos.position.z), .5f));
                    StartCoroutine(LerpToWantedRotation(belowObject.transform.localRotation));
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
                    playerSounds.hideAudioSource.PlayOneShot(playerSounds.hides[1]);
                    playerStateManager.isCrouching = true;
                    playerStateManager.isCrawling = false;
                    Transform hidePos = box.GetComponent<HideCloset>().hidePos;
                    playerStateManager.canInteract = false;
                    StartCoroutine(LerpToWantedPosition(transform.parent.position, new Vector3(hidePos.position.x, transform.parent.position.y, hidePos.position.z), .5f));
                    StartCoroutine(LerpToWantedRotation(box.transform.localRotation));
                    StartCoroutine(playerInputManager.LerpCameraPosition(playerInputManager.cam.transform.localPosition, new Vector3(playerInputManager.cam.transform.localPosition.x, playerInputManager.camYposCrouch - .2f, playerCam.camZpos), playerInputManager.camSpeed));
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
                    HideCloset hideCloset = closet.GetComponent<HideCloset>();
                    hideCloset.StartCoroutine(hideCloset.OpenCloseCoroutine());
                    playerStateManager.isCrouching = false;
                    playerStateManager.isCrawling = false;
                    playerCam.enabled = false;
                    Transform hidePos = closet.GetComponent<HideCloset>().hidePos;
                    playerStateManager.canInteract = false;
                    StartCoroutine(LerpToWantedPosition(transform.parent.position, new Vector3(hidePos.position.x, transform.parent.position.y, hidePos.position.z), .5f));
                    StartCoroutine(LerpToWantedRotation(closet.transform.localRotation));
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
                    animDoor.doorAudioSource.pitch = .8f;
                    animDoor.doorAudioSource.volume = .3f;
                    animDoor.doorAudioSource.PlayOneShot(animDoor.clipList[2]);
                    doorAnimator.SetTrigger("Locked");
                }
                if (animDoor.keyNumber == 1)
                {
                    if (!animDoor.doorOpen)
                    {
                        if (doorAnimator.speed != 0f)
                        {
                            animDoor.doorOpen = true;
                            animDoor.doorAudioSource.pitch = 2f;
                            animDoor.doorAudioSource.volume = .7f;
                            animDoor.doorAudioSource.PlayOneShot(animDoor.clipList[0]);
                        }
                    }
                    else if (doorAnimator.speed != 0f)
                    {
                        animDoor.doorOpen = false;
                        animDoor.doorAudioSource.pitch = 1.5f;
                        animDoor.doorAudioSource.volume = .7f;
                        animDoor.doorAudioSource.PlayOneShot(animDoor.clipList[1]);
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
        if (vent.tag == "Vent")
        {
            Vent _vent = vent.GetComponent<Vent>();

            if (_vent.screwDriverNumber == 0)
            {
                Debug.Log("Vent is close");
                _vent.VentLocked();
            }
            if (playerInventory.screwDriver > 0)
            {
                _vent.screwDriverNumber = playerInventory.screwDriver;
            }
            if (_vent.screwDriverNumber > 0)
            {
                //_vent.ventOpen = true;
                _vent.VentOpen();
                playerSounds.moodMuicAudioSource.PlayOneShot(playerSounds.openVentMusic);
            }
        }
    }

    private void TakeKey(GameObject key)
    {
        if (key.tag == "Key")
        {
            playerSounds.pickUpAudioSource.Play();
            key.GetComponent<Key>().KeyCollected();
            playerInventory.eventKeyOwned++;
        }
    }
    private void TakeScrewDriver(GameObject screwDriver)
    {
        if (screwDriver.tag == "ScrewDriver")
        {
            playerSounds.pickUpAudioSource.Play();
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
                        StartCoroutine(LerpToWantedPosition(transform.parent.position, new Vector3(focusPos.position.x, transform.parent.position.y, focusPos.position.z), .5f));
                        StartCoroutine(LerpToWantedRotation(eventObject.transform.localRotation));
                        playerCam.focusMaxDown = phoneMaxDown;
                        playerCam.focusMaxUp = phoneMaxUp;
                    }
                    if (eventObject.GetComponent<TVEventObject>() != null)
                    {
                        Transform focusPos = eventObject.GetComponent<TVEventObject>().focusPos;
                        playerStateManager.canInteract = false;
                        StartCoroutine(LerpToWantedPosition(transform.parent.position, new Vector3(focusPos.position.x, transform.parent.position.y, focusPos.position.z), .5f));
                        StartCoroutine(LerpToWantedRotation(eventObject.transform.localRotation));
                        playerCam.focusMaxDown = TVMaxDown;
                        playerCam.focusMaxUp = TVMaxUp;
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
            HideCloset hideCloset = playerPhysics.hideColliders[0].GetComponent<HideCloset>();
            hideCloset.StartCoroutine(hideCloset.OpenCloseCoroutine());
            playerSounds.hideAudioSource.PlayOneShot(playerSounds.hides[1]);
            Transform outPos = playerPhysics.hideColliders[0].GetComponent<HideCloset>().outPos;
            StartCoroutine(LerpToWantedPosition(transform.parent.position, new Vector3(outPos.position.x, transform.parent.position.y, outPos.position.z), .5f));
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
        RaycastHit hitInteract;
        RaycastHit hitNonInteract;
        bool hitInteractable = Physics.Raycast(transform.position, transform.forward, out hitInteract, grabRange, interactMask);
        bool hitNonInteractable = Physics.Raycast(transform.position, transform.forward, out hitNonInteract, grabRange, nonInteractMask);

        if (hitInteractable && (!hitNonInteractable || hitInteract.distance < hitNonInteract.distance))
        {
            // Si un objet interactif est détecté avant un objet non-interactif
            Debug.DrawLine(transform.position, hitInteract.point, Color.green);
            if (!isHiding)
                playerStateManager.canInteract = true;

            if (hitInteract.collider.gameObject.tag == "PickUpObj")
            {
                interactUIImage.sprite = pickUpImage;
            }
            else
            {
                interactUIImage.sprite = noPickUpImage;
            }
        }
        //else if (hitNonInteractable && (!hitInteractable || hitInteract.distance > hitNonInteract.distance))
        //{
        //    // Si un objet non-interactif est détecté avant un objet interactif
        //    Debug.DrawLine(transform.position, hitNonInteract.point, Color.red);
        //        playerStateManager.canInteract = false;
        //}
        else if (playerStateManager.state != PlayerStateManager.PlayerState.Hide)
        {
            // Aucun objet interactif détecté et l'état du joueur n'est pas "Hide"
            Debug.DrawLine(transform.position, transform.position + transform.forward * grabRange, Color.red);
            playerStateManager.canInteract = false;
        }
    }


    public bool RayDetectOther()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange, nonInteractMask))
        {
            return true;
        }

        return false;

    }

    public bool RayDetectInteraction()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange, interactMask))
        {
            return true;
        }

        return false;

    }


    public IEnumerator LerpToWantedPosition(Vector3 startPos, Vector3 endPos, float duration)
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

    private IEnumerator LerpToWantedRotation(Quaternion endRot)
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
