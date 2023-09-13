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


    public PlayerInputManager inputManager;
    public PlayerCam playerCam;

    public enum PlayerState
    {
        Idle,
        Walk,
        Run,
        Look
    }

    [Header("STATES")]
    [SerializeField] public PlayerState state;


    [Header("STATS")]
    public float walkSpeed;
    public float runSpeed;

    [Header("BOOLS")]
    public bool isRunning;

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
