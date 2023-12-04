using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    [Header("CAMERA SPEED")]
    [SerializeField] float sensX = 200f;
    [SerializeField] float sensY = 200f;

    [Header("CAMERA ROTATIONS")]
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float maxLookDown = 72f;
    [SerializeField] float maxLookUp = -80f;
    [SerializeField] float maxLookBackRight = 130f;
    [SerializeField] float maxLookBackLeft = -130f;
    [SerializeField] float focusMaxRight = 20f;
    [SerializeField] float focusMaxLeft = -20f;
    [SerializeField] float focusMaxUp = 20f;
    [SerializeField] float focusMaxDown = -20f;

    [Header("CAMERA POSITIONS")]
    [SerializeField] public float camZpos;
    [SerializeField] public float camZposCrawl;

    float rotationX;
    float rotationY;
    public Transform playerBody;
    public Vector3 targetRotationR;
    public Vector3 targetRotationL;
    public Vector3 targetPositionR;
    public Vector3 targetPositionL;

    [SerializeField] bool gamePadOn;
    float xRotation = 0f;

    public Vector2 lookInput;

    public bool isLooking;

    PlayerStateManager playerStateManager;
    PlayerInputManager playerInputManager;
    Grabber grabber;

    Camera cam;

    [SerializeField] Transform deadPoint;
    [SerializeField] GameObject mainCam;

    private void Awake()
    {
        playerStateManager = GetComponentInParent<PlayerStateManager>();
        playerInputManager = GetComponentInParent<PlayerInputManager>();
        cam = GetComponent<Camera>();
        grabber = GetComponent<Grabber>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.FindAction("LookBackR").started += OnStartLookBackR;
        inputActions.FindAction("LookBackR").canceled += OnStopLookBackR;
        inputActions.FindAction("LookBackL").started += OnStartLookBackL;
        inputActions.FindAction("LookBackL").canceled += OnStopLookBackL;

        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.FindAction("LookBackR").started -= OnStartLookBackR;
        inputActions.FindAction("LookBackR").canceled -= OnStopLookBackR;
        inputActions.FindAction("LookBackL").started -= OnStartLookBackL;
        inputActions.FindAction("LookBackL").canceled -= OnStopLookBackL;

        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnStartLookBackR(InputAction.CallbackContext ctx)
    {
        if (playerStateManager.isHiding || playerStateManager.isDead) return;
        StopAllCoroutines();
        isLooking = true;
        StartCoroutine(LerpRotationCam(transform.localRotation, Quaternion.Euler(targetRotationR), rotationSpeed, transform.localPosition, targetPositionR));
    }

    private void OnStopLookBackR(InputAction.CallbackContext ctx)
    {
        if (playerStateManager.isHiding || playerStateManager.isDead) return;
        StopAllCoroutines();
        StartCoroutine(LerpRotationCam(transform.localRotation, Quaternion.Euler(xRotation, 0f, 0f), rotationSpeed, transform.localPosition, new Vector3(0f, transform.localPosition.y, transform.localPosition.z)));
        StartCoroutine(EndLookBack());
    }

    private void OnStartLookBackL(InputAction.CallbackContext ctx)
    {
        if (playerStateManager.isHiding || playerStateManager.isDead) return;
        StopAllCoroutines();
        isLooking = true;
        StartCoroutine(LerpRotationCam(transform.localRotation, Quaternion.Euler(targetRotationL), rotationSpeed, transform.localPosition, targetPositionL));
    }
    private void OnStopLookBackL(InputAction.CallbackContext ctx)
    {
        if (playerStateManager.isHiding || playerStateManager.isDead) return;
        StopAllCoroutines();
        StartCoroutine(LerpRotationCam(transform.localRotation, Quaternion.Euler(xRotation, 0f, 0f), rotationSpeed, transform.localPosition, new Vector3(0f, transform.localPosition.y, transform.localPosition.z)));
        StartCoroutine(EndLookBack());
    }

    // Start is called before the first frame update
    void Start()
    {
        var devices = InputSystem.devices;

        foreach (var device in devices)
        {
            if (device is Gamepad)
            {
                Debug.Log("Manette déjà branchée");
                gamePadOn = true;
            }
        }

        transform.localRotation = Quaternion.identity;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added)
        {
            if (device is Gamepad)
            {
                Debug.Log("Manette branchée : " + device.displayName);
                gamePadOn = true;
            }
        }
        else if (change == InputDeviceChange.Removed)
        {
            if (device is Gamepad)
            {
                Debug.Log("Manette débranchée : " + device.displayName);
                gamePadOn = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerStateManager.isHiding)
        {
            CameraRotation();
        }
        SetCameraPositionAndRotation();

        //CameraOnFocus();
    }

    private void LateUpdate()
    {
        if (playerStateManager.isDead)
        {
            transform.localRotation = Quaternion.identity;
            cam.nearClipPlane = 0.01f;
            transform.parent = deadPoint;
        }
    }

    private void SetCameraPositionAndRotation()
    {
        if (playerInputManager.dirInput == Vector3.zero)
        {
            //Rot & Pos to the RIGHT
            targetRotationR = new Vector3(0f, 0f, -15f);
            targetPositionR = new Vector3(.6f, playerInputManager.camYposNormal, camZpos);

            //Rot & Pos to the LEFT
            targetRotationL = new Vector3(0f, 0f, 15f);
            targetPositionL = new Vector3(-.6f, playerInputManager.camYposNormal, camZpos);
        }
        //Look back
        else
        {
            //Rot & Pos to the RIGHT
            targetRotationR = new Vector3(0f, maxLookBackRight, -5f);
            targetPositionR = new Vector3(.3f, playerInputManager.camYposNormal, camZpos);

            //Rot & Pos to the LEFT
            targetRotationL = new Vector3(0f, maxLookBackLeft, 5f);
            targetPositionL = new Vector3(-.3f, playerInputManager.camYposNormal, camZpos);
        }

        //CROUCH IDLE
        if (playerStateManager.state == PlayerStateManager.PlayerState.CrouchIdle)
        {
            //Rot & Pos to the RIGHT
            targetRotationR = new Vector3(0f, 0f, -15f);
            targetPositionR = new Vector3(.6f, playerInputManager.camYposCrouch, camZpos);

            //Rot & Pos to the LEFT
            targetRotationL = new Vector3(0f, 0f, 15f);
            targetPositionL = new Vector3(-.6f, playerInputManager.camYposCrouch, camZpos);
        }

        //CROUCH
        if (playerStateManager.state == PlayerStateManager.PlayerState.Crouch)
        {
            //Rot & Pos to the RIGHT
            targetRotationR = new Vector3(0f, maxLookBackRight, -5f);
            targetPositionR = new Vector3(.3f, playerInputManager.camYposCrouch, camZpos);

            //Rot & Pos to the LEFT
            targetRotationL = new Vector3(0f, maxLookBackLeft, 5f);
            targetPositionL = new Vector3(-.3f, playerInputManager.camYposCrouch, camZpos);
        }

        //CRAWL IDLE
        if (playerStateManager.state == PlayerStateManager.PlayerState.CrawlIdle)
        {
            //Rot & Pos to the RIGHT
            targetRotationR = new Vector3(0f, 0f, -15f);
            targetPositionR = new Vector3(.6f, playerInputManager.camYposCrawl, camZpos);

            //Rot & Pos to the LEFT
            targetRotationL = new Vector3(0f, 0f, 15f);
            targetPositionL = new Vector3(-.6f, playerInputManager.camYposCrawl, camZpos);
        }

        //CRAWL
        if (playerStateManager.state == PlayerStateManager.PlayerState.Crawl)
        {
            //Rot & Pos to the RIGHT
            targetRotationR = new Vector3(0f, maxLookBackRight, -5f);
            targetPositionR = new Vector3(.4f, playerInputManager.camYposCrawl, camZposCrawl);

            //Rot & Pos to the LEFT
            targetRotationL = new Vector3(0f, maxLookBackLeft, 5f);
            targetPositionL = new Vector3(-.4f, playerInputManager.camYposCrawl, camZposCrawl);
        }
    }

    public void CameraOnFocus()
    {
        rotationY += sensX * Input.GetAxis("Mouse X") * Time.deltaTime;
        rotationX -= sensY * Input.GetAxis("Mouse Y") * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, focusMaxLeft, focusMaxRight);
        rotationY = Mathf.Clamp(rotationY, focusMaxDown, focusMaxUp);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0f);

    }

    private void CameraRotation()
    {
        //Return si LookBack
        if (isLooking || grabber.isHiding) return;
        //get mouse input
        //Add mouse sensitivity
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        if (gamePadOn)
        {
            lookInput = inputActions.FindAction("Look").ReadValue<Vector2>();
            mouseX = lookInput.x * sensX * Time.deltaTime;
            mouseY = lookInput.y * sensY * Time.deltaTime;
        }

        //Limit view on Y axis
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, maxLookUp, maxLookDown);

        //Apply
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        if (playerStateManager.isDead)
        {
            cam.nearClipPlane = 0.01f;
        }
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
