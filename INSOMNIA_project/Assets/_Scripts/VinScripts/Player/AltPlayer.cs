using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPlayer
{
    //scriptable object
    float _walkSpeed = 5f;
    float _sprintSpeed = 8f;
    float _crouchSpeed = 3f;

    float _speed;

    public float Speed 
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value * 10f;
        }
    }



    public enum AltPlayerState
    {
        IDLE,
        WALK,
        SPRINT,
        CROUCHIDLE,
        CROUCH,
        FALL
    }

    AltPlayerState _currentState;

    public AltPlayerState CurrentState 
    { 
        get 
        { 
            return _currentState; 
        } 
        set 
        {
            _currentState = value; 
        } 
    }


    public AltPlayer()
    {
        Speed = 0f;
        CurrentState = AltPlayerState.IDLE;
    }


    public void AltPlayerOnStateEnter()
    {
        switch (_currentState)
        {
            case AltPlayerState.IDLE:
                Speed = 0f;
                break;
            case AltPlayerState.WALK:
                Speed = _walkSpeed;
                break;
            case AltPlayerState.SPRINT:
                Speed = _sprintSpeed;
                break;
            case AltPlayerState.CROUCHIDLE:
                Speed = 0f;
                break;
            case AltPlayerState.CROUCH:
                Speed = _crouchSpeed;
                break;
            case AltPlayerState.FALL:
                break;
        }
    }

    public void AltPlayerOnStateUpdate()
    {
        switch (_currentState)
        {
            case AltPlayerState.IDLE:
                break;
            case AltPlayerState.WALK:
                break;
            case AltPlayerState.SPRINT:
                break;
            case AltPlayerState.CROUCHIDLE:
                break;
            case AltPlayerState.CROUCH:
                break;
            case AltPlayerState.FALL:
                break;
        }
    }

    public void AltPlayerOnStateExit()
    {
        switch (_currentState)
        {
            case AltPlayerState.IDLE:
                break;
            case AltPlayerState.WALK:
                break;
            case AltPlayerState.SPRINT:
                break;
            case AltPlayerState.CROUCHIDLE:
                break;
            case AltPlayerState.CROUCH:
                break;
            case AltPlayerState.FALL:
                break;
        }
    }

    public void AltPlayerTransitionToState(AltPlayerState nextState)
    {
        AltPlayerOnStateExit();
        CurrentState = nextState;
        AltPlayerOnStateEnter();
    }

    public void Move(Rigidbody rb, Vector3 motion)
    {
        rb.velocity += motion.normalized * Speed * Time.deltaTime;
    }
}
