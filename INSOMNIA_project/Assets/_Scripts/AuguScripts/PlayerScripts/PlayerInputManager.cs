using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] public InputActionAsset inputActions;

    CharacterController characterController;
    PlayerStateManager playerStateManager;
    PlayerCam playerCam;

    public GameObject cam;
    public float speed;

    Vector3 moveInput;
    public Vector3 dirInput;
    

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

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerStateManager = GetComponent<PlayerStateManager>();
        playerCam = GetComponentInParent<PlayerCam>();
    }

    private void OnStartRun (InputAction.CallbackContext ctx)
    {
        playerStateManager.isRunning = true;
    }
    private void OnStopRun(InputAction.CallbackContext ctx)
    {
        playerStateManager.isRunning = false;
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
            StartCoroutine(LerpCameraToCrouch(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, .2f, cam.transform.localPosition.z), .2f));
        }


        //if (!playerStateManager.isCrouching)
        //{
        //    StartCoroutine(LerpCameraToCrouch(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, .61f, cam.transform.localPosition.z), .2f));
        //}
    }

    public IEnumerator LerpCameraToCrouch(Vector3 startPos, Vector3 endPos, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            cam.transform.localPosition = Vector3.Lerp(startPos, endPos, t/duration);
            t+= Time.deltaTime;

            yield return null;
        }

        cam.transform.localPosition = endPos;
    }
}
