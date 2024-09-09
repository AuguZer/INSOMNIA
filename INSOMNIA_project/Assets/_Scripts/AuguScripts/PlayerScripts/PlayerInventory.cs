using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public int keyOwned = 0;
    [SerializeField] public int eventKeyOwned = 0;
    [SerializeField] public int screwDriver;
    // Start is called before the first frame update
    void Start()
    {
        keyOwned = 0;
        eventKeyOwned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
