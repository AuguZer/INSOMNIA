using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] public InputActionAsset inputActions;

    public CharacterController characterController;
    PlayerStateManager playerStateManager;
    PlayerCam playerCam;


    public GameObject cam;
    public float speed;

    Vector3 moveInput;
    public Vector3 dirInput;

    [Header("CAMERA POSITION & SPEED")]
    [SerializeField] float camYposNormal = .61f;
    [SerializeField] float camYposCrouch = .12f;
    [SerializeField] float camYposCrawl = .05f;
    [SerializeField] float camSpeed = .2f;


    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.FindAction("Run").started += OnStartRun;
        inputActions.FindAction("Run").canceled += OnStopRun;
        //inputActions.FindAction("Crouch").performed += OnStartCrouch;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.FindAction("Run").started -= OnStartRun;
        inputActions.FindAction("Run").canceled -= OnStopRun;
        //inputActions.FindAction("Crouch").performed -= OnStartCrouch;

    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerStateManager = GetComponent<PlayerStateManager>();
        playerCam = GetComponentInParent<PlayerCam>();

    }

    private void OnStartRun(InputAction.CallbackContext ctx)
    {
        playerStateManager.isRunning = true;
    }
    private void OnStopRun(InputAction.CallbackContext ctx)
    {
        playerStateManager.isRunning = false;
    }
    private void OnStartCrouch(InputAction.CallbackContext ctx)
    {
        playerStateManager.isCrouching = true;
    }
    private void OnStopCrouch(InputAction.CallbackContext ctx)
    {
    
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
        CrouchInput();
        CrawlInput();
    }

    private void MoveInput()
    {
        moveInput = inputActions.FindAction("Move").ReadValue<Vector2>();
        dirInput = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 move = transform.right * dirInput.x + transform.forward * dirInput.z;

        characterController.Move(move.normalized * speed * Time.deltaTime);
    }

    private void CrouchInput()
    {
        if (inputActions.FindAction("Crouch").WasPerformedThisFrame())
        {
            playerStateManager.isCrouching = true;
            playerStateManager.isCrawling = false;
            StartCoroutine(LerpCameraPosition(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, camYposCrouch, cam.transform.localPosition.z), camSpeed));

            if (playerStateManager.state == PlayerStateManager.PlayerState.CrouchIdle || playerStateManager.state == PlayerStateManager.PlayerState.Crouch)
            {
                playerStateManager.isCrouching = false;
                StartCoroutine(LerpCameraPosition(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, camYposNormal, cam.transform.localPosition.z), camSpeed));
            }



        }
    }
    private void CrawlInput()
    {
        if (inputActions.FindAction("Crawl").WasPerformedThisFrame())
        {
            playerStateManager.isCrawling = true;
            playerStateManager.isCrouching = false;
            StartCoroutine(LerpCameraPosition(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, camYposCrawl, cam.transform.localPosition.z), camSpeed));

            if (playerStateManager.state == PlayerStateManager.PlayerState.CrawlIdle || playerStateManager.state == PlayerStateManager.PlayerState.Crawl)
            {
                playerStateManager.isCrawling = false;
                StartCoroutine(LerpCameraPosition(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, camYposNormal, cam.transform.localPosition.z), camSpeed));
            }
        }
    }






    //private void CrouchInput()
    //{
    //    if (inputActions.FindAction("Crouch").WasReleasedThisFrame())
    //    {
    //        //CROUCH TO STAND
    //        if (playerStateManager.state == PlayerStateManager.PlayerState.CrouchIdle || playerStateManager.state == PlayerStateManager.PlayerState.Crouch)
    //        {
    //            playerStateManager.isCrouching = false;
    //            StartCoroutine(LerpCameraPosition(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, camYposNormal, cam.transform.localPosition.z), camSpeed));
    //        }

    //        else if (playerStateManager.state == PlayerStateManager.PlayerState.CrawlIdle || playerStateManager.state == PlayerStateManager.PlayerState.Crawl)
    //        {
    //            playerStateManager.isCrawling = false;
    //            StartCoroutine(LerpCameraPosition(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, camYposCrouch, cam.transform.localPosition.z), camSpeed));
    //        }
    //        else
    //        {
    //            //CROUCH
    //            //if (playerStateManager.posNumber == 0)
    //            //{
    //                playerStateManager.isCrouching = true;
    //                StartCoroutine(LerpCameraPosition(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, camYposCrouch, cam.transform.localPosition.z), camSpeed));
    //            //}
    //        }
    //    }
    //}

    //private void CrawlInput()
    //{
    //    if (inputActions.FindAction("Crawl").WasPerformedThisFrame())
    //    {
    //        //if (playerStateManager.posNumber == 0)
    //        //{
    //            playerStateManager.posNumber++;
    //            playerStateManager.isCrawling = true;
    //            //CRAWL
    //            StartCoroutine(LerpCameraPosition(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, camYposCrawl, cam.transform.localPosition.z), camSpeed));

    //        //}

    //        //CRAWL TO STAND
    //        //if (playerStateManager.posNumber > 0)
    //        //{
    //            if (playerStateManager.state == PlayerStateManager.PlayerState.CrawlIdle || playerStateManager.state == PlayerStateManager.PlayerState.Crawl)
    //            {
    //                playerStateManager.isCrawling = false;
    //                playerStateManager.isCrouching = false;
    //                StartCoroutine(LerpCameraPosition(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, camYposNormal, cam.transform.localPosition.z), camSpeed));
    //            }
    //            if (playerStateManager.state == PlayerStateManager.PlayerState.CrouchIdle || playerStateManager.state == PlayerStateManager.PlayerState.Crouch)
    //            {
    //                if (!playerStateManager.isCrawling)
    //                {
    //                    StartCoroutine(LerpCameraPosition(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, camYposNormal, cam.transform.localPosition.z), camSpeed));
    //                }
    //            }
    //        //}


    //        //if (playerStateManager.state == PlayerStateManager.PlayerState.CrouchIdle || playerStateManager.state == PlayerStateManager.PlayerState.Crouch)
    //        //{
    //        //    StartCoroutine(LerpCameraPosition(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, camYposCrawl, cam.transform.localPosition.z), camSpeed));
    //        //}
    //    }
    //}

    public IEnumerator LerpCameraPosition(Vector3 startPos, Vector3 endPos, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            cam.transform.localPosition = Vector3.Lerp(startPos, endPos, t / duration);
            t += Time.deltaTime;

            yield return null;
        }

        cam.transform.localPosition = endPos;
    }

    IEnumerator CrouchToCrawl()
    {
        yield return new WaitForSeconds(.5f);


    }
}
