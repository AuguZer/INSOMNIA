using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetAltPlayerInputs : MonoBehaviour
{
    [SerializeField] InputActionAsset _altPlayerInputs;
    bool _sprintInput;
    bool _crouchInput;

    public bool SprintInput { get; private set; }
    public bool CrouchInput {  get; private set; }

    private void OnEnable()
    {
        _altPlayerInputs.Enable();
        _altPlayerInputs.FindAction("Sprint").started += OnStartSprinting;
        _altPlayerInputs.FindAction("Sprint").canceled += OnStopSprinting;
        _altPlayerInputs.FindAction("Crouch").started += SwitchCrouch;
    }

    private void OnDisable()
    {
        _altPlayerInputs.Disable();
        _altPlayerInputs.FindAction("Sprint").started -= OnStartSprinting;
        _altPlayerInputs.FindAction("Sprint").canceled -= OnStopSprinting;
        _altPlayerInputs.FindAction("Crouch").started -= SwitchCrouch;
    }
    
    public Vector3 MoveInput()
    {
        Vector2 moveInput = _altPlayerInputs.FindAction("Move").ReadValue<Vector2>();
        return new Vector3(moveInput.x, 0f, moveInput.y);
    }

    void OnStartSprinting(InputAction.CallbackContext ctx)
    {
        SprintInput = true;
    }

    void OnStopSprinting(InputAction.CallbackContext ctx)
    {
        SprintInput = false;
    }

    void SwitchCrouch(InputAction.CallbackContext ctx)
    {
        CrouchInput = !CrouchInput;
    }

    public void ForceStopSprint()
    {
        SprintInput = false;
    }
}
