using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    public PlayerIdle Idle = new PlayerIdle();
    public PlayerWalk Walk = new PlayerWalk();
    public PlayerLook Look = new PlayerLook();
    public PlayerRun Run = new PlayerRun();
    public PlayerCrouchIdle CrouchIdle = new PlayerCrouchIdle();
    public PlayerCrouch Crouch = new PlayerCrouch();
    public PlayerCrawlIdle CrawlIdle = new PlayerCrawlIdle();
    public PlayerCrawl Crawl = new PlayerCrawl();
    public PlayerJump Jump = new PlayerJump();
    public PlayerFall Fall = new PlayerFall();
    public PlayerLand Land = new PlayerLand();
    public PlayerHide Hide = new PlayerHide();
    public PlayerDeath Death = new PlayerDeath();


    public PlayerInputManager inputManager;
    public PlayerCam playerCam;
    public PlayerPhysics playerPhysics;

    [SerializeField] public GameObject graphics;

    public enum PlayerState
    {
        Idle,
        Walk,
        Run,
        CrouchIdle,
        Crouch,
        CrawlIdle,
        Crawl,
        Jump,
        Fall,
        Land,
        Look,
        Hide,
        Death,
    }

    [Header("STATES")]
    [SerializeField] public PlayerState state;


    [Header("STATS")]
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float crawlSpeed;
    public float backwardDivider;

    [Header("BOOLS")]
    public bool isRunning;
    public bool isCrouching;
    public bool isCrawling;
    public bool isHiding;
    public bool canInteract;
    public bool isDead;
    public bool isJumping;
    public bool isFalling;
    public bool isLanding;
    public bool getUpFromLanding;

    public Rigidbody rb;
    [SerializeField] public Transform deadPoint;
    [SerializeField] public GameObject mainCamera;

    [SerializeField] public Vector3 landCamPos;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        playerCam = GetComponentInChildren<PlayerCam>();
        playerPhysics = GetComponent<PlayerPhysics>();
   
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = Idle;
        currentState.OnStateEnter(this);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate(this);

    }

    public void TransitionToState(PlayerBaseState nextState)
    {
        currentState.OnStateExit(this);
        currentState = nextState;
        currentState.OnStateEnter(this);
    }

    public IEnumerator GetupFromLand()
    {
        yield return new WaitForSeconds(1f);
        getUpFromLanding = true;

    }
    public IEnumerator LandCoroutine()
    {
        isLanding = true;
        yield return new WaitForSeconds(1.5f);
        isLanding = false;

    }
   public IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(3f);
        playerCam.enabled = false;
    }
}
