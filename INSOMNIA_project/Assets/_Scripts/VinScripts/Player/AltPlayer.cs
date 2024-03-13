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

    public AltPlayer()
    {
        _currentSpeed = PlayerSpeed.NOSPEED;
    }

    public enum PlayerSpeed
    {
        NOSPEED,
        WALKSPEED,
        SPRINTSPEED,
        CROUCHSPEED
    }

    PlayerSpeed _currentSpeed;

    public void SetSpeed(PlayerSpeed playerSpeed)
    {
        _currentSpeed = playerSpeed;

        switch (_currentSpeed)
        {
            case PlayerSpeed.NOSPEED:
                _speed = 0f;
                break;
            case PlayerSpeed.WALKSPEED:
                _speed = _walkSpeed;
                break;
            case PlayerSpeed.SPRINTSPEED:
                _speed = _sprintSpeed;
                break;
            case PlayerSpeed.CROUCHSPEED:
                _speed = _crouchSpeed;
                break;
        }
    }

    public void Move(CharacterController cc, Vector3 move)
    {
        Vector3 motion = move.normalized * _speed * Time.deltaTime;
        cc.Move(motion);
    }
}
