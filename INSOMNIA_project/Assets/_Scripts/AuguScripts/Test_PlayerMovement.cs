using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_PlayerMovement : MonoBehaviour
{

    [SerializeField] InputActionAsset inputActions;

    CharacterController characterController;

    Vector2 moveInput;
    Vector3 dirInput;



    [SerializeField] float walkSpeed = 1f;
    [SerializeField] float sprintSpeed = 1f;

    bool isRunning;
    bool isCrouching;


    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.FindAction("Run").started += OnStartRun;
        inputActions.FindAction("Run").canceled += OnStopRun;
        inputActions.FindAction("Crouch").started += OnStartCrouch;
        inputActions.FindAction("Crouch").canceled += OnStopCrouch;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.FindAction("Run").started -= OnStartRun;
        inputActions.FindAction("Run").canceled -= OnStopRun;
        inputActions.FindAction("Crouch").started -= OnStartCrouch;
        inputActions.FindAction("Crouch").canceled -= OnStopCrouch;
    }

    private void OnStartRun(InputAction.CallbackContext ctx)
    {
        isRunning = true;
    }
    private void OnStopRun(InputAction.CallbackContext ctx)
    {
        isRunning = false;
    }

    private void OnStartCrouch(InputAction.CallbackContext ctx)
    {
        isCrouching = true;
    }
    private void OnStopCrouch(InputAction.CallbackContext ctx)
    {
        isCrouching = false;
    }
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Crouch();
    }

    private void Move()
    {
        moveInput = inputActions.FindAction("Move").ReadValue<Vector2>();
        dirInput = new Vector3(moveInput.x, 0, moveInput.y);

        Vector3 move = transform.right * dirInput.x + transform.forward * dirInput.z;


        float actualSpeed;
        if (isRunning) actualSpeed = sprintSpeed;
        else actualSpeed = walkSpeed;

        characterController.Move(move.normalized * actualSpeed * Time.deltaTime);
    }

    private void Crouch()
    {
        if (isCrouching)
        {
            characterController.height = 1.2f;
            characterController.center = new Vector3(0f, -.2f, 0f);
        }
        else
        {
            characterController.height = 2f;
            characterController.center = Vector3.zero;
        }
    }


}
