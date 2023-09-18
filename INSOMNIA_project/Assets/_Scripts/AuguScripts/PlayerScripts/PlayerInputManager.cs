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
    [SerializeField] public float camYposNormal = .61f;
    [SerializeField] public float camYposCrouch = .12f;
    [SerializeField] public float camYposCrawl = .05f;
    [SerializeField] public float camSpeed = .2f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerStateManager = GetComponent<PlayerStateManager>();
        playerCam = GetComponentInParent<PlayerCam>();

    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.FindAction("Run").started += OnStartRun;
        inputActions.FindAction("Run").canceled += OnStopRun;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.FindAction("Run").started -= OnStartRun;
        inputActions.FindAction("Run").canceled -= OnStopRun;
    }

    private void OnStartRun(InputAction.CallbackContext ctx)
    {
        playerStateManager.isRunning = true;
    }
    private void OnStopRun(InputAction.CallbackContext ctx)
    {
        playerStateManager.isRunning = false;
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
}
