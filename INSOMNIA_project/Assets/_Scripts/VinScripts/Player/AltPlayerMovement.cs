using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPlayerMovement : MonoBehaviour
{
    AltPlayer _player;
    GetAltPlayerInputs _inputsScript;
    Rigidbody _rb;

    Vector3 moveInput;


    // Start is called before the first frame update
    void Start()
    {
        _player = new AltPlayer();
        _inputsScript = GetComponent<GetAltPlayerInputs>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _player.AltPlayerOnStateUpdate();
        _inputsScript.ProcessInputs(out moveInput);

        if(moveInput != Vector3.zero)
        {
            _player.AltPlayerTransitionToState(AltPlayer.AltPlayerState.WALK);
        }
    }

    private void FixedUpdate()
    {
        if (moveInput != Vector3.zero)
        {
            _player.Move(_rb, moveInput);
        }
    }
}
