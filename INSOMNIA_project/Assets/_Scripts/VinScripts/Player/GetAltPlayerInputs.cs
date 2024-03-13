using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetAltPlayerInputs : MonoBehaviour
{
    [SerializeField] InputActionAsset _altPlayerInputs;

    private void OnEnable()
    {
        _altPlayerInputs.Enable();
    }

    private void OnDisable()
    {
        _altPlayerInputs.Disable();
    }

    public void ProcessInputs(out Vector3 moveVector)
    {
        moveVector = MoveInput();
    }
    
    public Vector3 MoveInput()
    {
        Vector2 moveInput = _altPlayerInputs.FindAction("Move").ReadValue<Vector2>();
        return new Vector3(moveInput.x, 0f, moveInput.y);
    }
}
