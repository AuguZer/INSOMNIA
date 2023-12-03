using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventsManager : MonoBehaviour
{
    public UnityEvent PlayerDeath;
    public UnityEvent EnableInteractUI;
    public UnityEvent DisableInteractUI;

    PlayerStateManager playerStateManager;

    private void Awake()
    {
        playerStateManager = GetComponent<PlayerStateManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStateManager.isDead)
        {
            PlayerDeath?.Invoke();
        }
    }
}
