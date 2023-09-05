using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    [SerializeField] float sensX = 200f;
    [SerializeField] float sensY = 200f;
    [SerializeField] float rotationSpeed = 5f;

    public Transform playerBody;

    [SerializeField] Transform lookBackPoint;

    Vector3 targetRotation;

    float xRotation;


    Vector2 lookInput;

    bool isLookingBack;

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.FindAction("LookBack").started += OnStartLookBack;
        inputActions.FindAction("LookBack").canceled += OnStopLookBack;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.FindAction("LookBack").started -= OnStartLookBack;
        inputActions.FindAction("LookBack").canceled -= OnStopLookBack;
    }

    private void OnStartLookBack(InputAction.CallbackContext ctx)
    {
        isLookingBack = true;
        StartCoroutine(LerpRotationCam(transform.localRotation, Quaternion.Euler(targetRotation), rotationSpeed));
    }

    private void OnStopLookBack(InputAction.CallbackContext ctx)
    {
        StopAllCoroutines();
        isLookingBack = false;
        StartCoroutine(LerpRotationCam(Quaternion.Euler(targetRotation), Quaternion.Euler(new Vector3(0f, 0f, 0f)), rotationSpeed));
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation = new Vector3(transform.localRotation.x, 145f, 0f);
        if (isLookingBack) return;
        //get mouse input
        lookInput = inputActions.FindAction("Look").ReadValue<Vector2>();
        //Add mouse sensitivity
        float mouseX = lookInput.x * sensX * Time.deltaTime;
        float mouseY = lookInput.y * sensY * Time.deltaTime;

        //Limit view on Y axis
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //Apply
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

 

    }

    private void LookBack()
    {
        if (isLookingBack)
        {
            //transform.LookAt(look);
        }
    }

    IEnumerator LerpRotationCam(Quaternion startValue, Quaternion endValue, float duration)
    {
        float t = 0f;
        //Quaternion startValue = transform.localRotation;

        while (t < duration)
        {
            transform.localRotation = Quaternion.Lerp(startValue, endValue, t / duration);
            t += Time.deltaTime;

            yield return null;
        }
        transform.localRotation = endValue;
    }
}
