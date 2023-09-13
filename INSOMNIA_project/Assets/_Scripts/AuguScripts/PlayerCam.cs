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
    public Vector3 targetRotationR;
    public Vector3 targetRotationL;
    public Vector3 targetPositionR;
    public Vector3 targetPositionL;

    float xRotation;

    public float xRot;


    public Vector2 lookInput;

    public bool isLooking;

    PlayerStateManager playerStateManager;
    PlayerInputManager playerInputManager;

    private void Awake()
    {
     playerStateManager = GetComponentInParent<PlayerStateManager>();
        playerInputManager = GetComponentInParent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.FindAction("LookBackR").started += OnStartLookBackR;
        inputActions.FindAction("LookBackR").canceled += OnStopLookBackR;
        inputActions.FindAction("LookBackL").started += OnStartLookBackL;
        inputActions.FindAction("LookBackL").canceled += OnStopLookBackL;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.FindAction("LookBackR").started -= OnStartLookBackR;
        inputActions.FindAction("LookBackR").canceled -= OnStopLookBackR;
        inputActions.FindAction("LookBackL").started -= OnStartLookBackL;
        inputActions.FindAction("LookBackL").canceled -= OnStopLookBackL;
    }

    private void OnStartLookBackR(InputAction.CallbackContext ctx)
    {
        StopAllCoroutines();
        isLooking = true;
        StartCoroutine(LerpRotationCam(transform.localRotation, Quaternion.Euler(targetRotationR), rotationSpeed, transform.localPosition, targetPositionR));
    }

    private void OnStopLookBackR(InputAction.CallbackContext ctx)
    {
        StopAllCoroutines();
        StartCoroutine(LerpRotationCam(transform.localRotation, Quaternion.Euler(xRotation, 0f, 0f), rotationSpeed, transform.localPosition, new Vector3(0f, transform.localPosition.y, transform.localPosition.z)));
        StartCoroutine(EndLookBack());
    }

    private void OnStartLookBackL(InputAction.CallbackContext ctx)
    {
        StopAllCoroutines();
        isLooking = true;
        StartCoroutine(LerpRotationCam(transform.localRotation, Quaternion.Euler(targetRotationL), rotationSpeed, transform.localPosition, targetPositionL));
    }
    private void OnStopLookBackL(InputAction.CallbackContext ctx)
    {
        StopAllCoroutines();
        StartCoroutine(LerpRotationCam(transform.localRotation, Quaternion.Euler(xRotation, 0f, 0f), rotationSpeed, transform.localPosition, new Vector3(0f, transform.localPosition.y, transform.localPosition.z)));
        StartCoroutine(EndLookBack());
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
        //Look on side
        if (playerInputManager.dirInput == Vector3.zero)
        {
            //Rot & Pos to the RIGHT
            targetRotationR = new Vector3(0f, 0f, -15f);
            targetPositionR = new Vector3(.6f, .61f, .451f);

            //Rot & Pos to the LEFT
            targetRotationL = new Vector3(0f, 0f, 15f);
            targetPositionL = new Vector3(-.6f, .61f,.451f);
        }
        //Look back
        else
        {
            //Rot & Pos to the RIGHT
            targetRotationR = new Vector3(0f, 130f, -5f);
            targetPositionR = new Vector3(0f, .61f, .451f);

            //Rot & Pos to the LEFT
            targetRotationL = new Vector3(0f, -130f, 5f);
            targetPositionL = new Vector3(0f, .61f, .451f);
        }

        //Return si LookBack
        if (isLooking) return;
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


    IEnumerator LerpRotationCam(Quaternion startValue, Quaternion endValue, float duration, Vector3 startPos, Vector3 endPos)
    {
        float t = 0f;
        //Quaternion startValue = transform.localRotation;

        while (t < duration)
        {
            transform.localRotation = Quaternion.Lerp(startValue, endValue, t / duration);
            transform.localPosition = Vector3.Lerp(startPos, endPos, t / duration);
            t += Time.deltaTime;

            yield return null;
        }
        transform.localRotation = endValue;
        transform.localPosition = endPos;
    }

    IEnumerator EndLookBack()
    {
        yield return new WaitForSeconds(rotationSpeed);
        isLooking = false;
    }
}
