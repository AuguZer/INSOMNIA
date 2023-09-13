using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    CharacterController characterController;
    PlayerStateManager playerStateManager;

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
    }

    private void MoveInput()
    {
        moveInput = inputActions.FindAction("Move").ReadValue<Vector2>();
        dirInput = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 move = transform.right * dirInput.x + transform.forward * dirInput.z;

        characterController.Move(move.normalized * speed * Time.deltaTime);
    }
}
