using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPlayerMovement : MonoBehaviour
{
    AltPlayer _player;
    GetAltPlayerInputs _inputsScript;
    CharacterController _charaController;

    enum AltPlayerState
    {
        IDLE,
        WALK,
        SPRINT,
        CROUCHIDLE,
        CROUCH,
        FALL
    }

    AltPlayerState _currentState;

    // Start is called before the first frame update
    void Start()
    {
        _player = new AltPlayer();
        _inputsScript = GetComponent<GetAltPlayerInputs>();
        _charaController = GetComponent<CharacterController>();
        _currentState = AltPlayerState.IDLE;
        OnStateEnter();
    }


    private void Update()
    {
        Debug.Log(_currentState);
        OnStateUpdate();
    }

    private void FixedUpdate()
    {
        _player.Move(_charaController, _inputsScript.MoveInput());
    }

    void OnStateEnter()
    {
        switch (_currentState)
        {
            case AltPlayerState.IDLE:
                _player.SetSpeed(AltPlayer.PlayerSpeed.NOSPEED);
                _charaController.height = 2f;
                break;
            case AltPlayerState.WALK:
                _player.SetSpeed(AltPlayer.PlayerSpeed.WALKSPEED);
                _charaController.height = 2f;
                break;
            case AltPlayerState.SPRINT:
                _player.SetSpeed(AltPlayer.PlayerSpeed.SPRINTSPEED);
                _charaController.height = 2f;
                break;
            case AltPlayerState.CROUCHIDLE:
                _player.SetSpeed(AltPlayer.PlayerSpeed.NOSPEED);
                _inputsScript.ForceStopSprint();
                _charaController.height = 1f;
                break;
            case AltPlayerState.CROUCH:
                _player.SetSpeed(AltPlayer.PlayerSpeed.CROUCHSPEED);
                _inputsScript.ForceStopSprint();
                _charaController.height = 1f;
                break;
            case AltPlayerState.FALL:
                _player.SetSpeed(AltPlayer.PlayerSpeed.NOSPEED);
                break;
        }
    }

    void OnStateUpdate()
    {
        switch (_currentState)
        {
            case AltPlayerState.IDLE:

                if(_inputsScript.MoveInput() != Vector3.zero && !_inputsScript.SprintInput)
                {
                    TransitionToState(AltPlayerState.WALK);
                }
                
                if(_inputsScript.MoveInput() != Vector3.zero && _inputsScript.SprintInput)
                {
                    TransitionToState(AltPlayerState.SPRINT);
                }

                if(_inputsScript.MoveInput() == Vector3.zero && _inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.CROUCHIDLE);
                }

                if (_inputsScript.MoveInput() != Vector3.zero && _inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.CROUCH);
                }

                break;
            case AltPlayerState.WALK:

                if (_inputsScript.MoveInput() == Vector3.zero && !_inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.IDLE);
                }
                
                if (_inputsScript.MoveInput() == Vector3.zero && _inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.CROUCHIDLE);
                }

                if (_inputsScript.MoveInput() != Vector3.zero && _inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.CROUCH);
                }

                if (_inputsScript.MoveInput() != Vector3.zero && _inputsScript.SprintInput)
                {
                    TransitionToState(AltPlayerState.SPRINT);
                }

                break;
            case AltPlayerState.SPRINT:

                if (_inputsScript.MoveInput() == Vector3.zero && !_inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.IDLE);
                }

                if (_inputsScript.MoveInput() == Vector3.zero && _inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.CROUCHIDLE);
                }

                if (_inputsScript.MoveInput() != Vector3.zero && !_inputsScript.SprintInput)
                {
                    TransitionToState(AltPlayerState.WALK);
                }

                if (_inputsScript.MoveInput() != Vector3.zero && _inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.CROUCH);
                }

                break;
            case AltPlayerState.CROUCHIDLE:

                if(_inputsScript.MoveInput() == Vector3.zero && !_inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.IDLE);
                }

                if (_inputsScript.MoveInput() != Vector3.zero && !_inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.WALK);
                }

                if (_inputsScript.MoveInput() != Vector3.zero && _inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.CROUCH);
                }

                break;
            case AltPlayerState.CROUCH:

                if (_inputsScript.MoveInput() == Vector3.zero && !_inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.IDLE);
                }

                if (_inputsScript.MoveInput() != Vector3.zero && !_inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.WALK);
                }

                if (_inputsScript.MoveInput() == Vector3.zero && _inputsScript.CrouchInput)
                {
                    TransitionToState(AltPlayerState.CROUCHIDLE);
                }

                break;
            case AltPlayerState.FALL:
                break;
        }
    }

    void OnStateExit()
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

    void TransitionToState(AltPlayerState nextState)
    {
        OnStateExit();
        _currentState = nextState;
        OnStateEnter();
    }
}
