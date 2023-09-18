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


    public PlayerInputManager inputManager;
    public PlayerCam playerCam;

    public enum PlayerState
    {
        Idle,
        Walk,
        Run,
        CrouchIdle,
        Crouch,
        CrawlIdle,
        Crawl,
        Look
    }

    [Header("STATES")]
    [SerializeField] public PlayerState state;


    [Header("STATS")]
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;

    [Header("BOOLS")]
    public bool isRunning;
    public bool isCrouching;
    public bool isCrawling;
    public bool isStanding;


    public int posNumber;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        playerCam = GetComponentInChildren<PlayerCam>();
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
}
