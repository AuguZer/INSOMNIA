using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void OnStateEnter(PlayerStateManager playerState);
    public abstract void OnStateUpdate(PlayerStateManager playerState);
    public abstract void OnStateExit(PlayerStateManager playerState);
   
}
