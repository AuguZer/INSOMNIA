using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventKeyDesk8 : Key
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void KeyCollected()
    {
        base.KeyCollected();
        Debug.Log("Activer l'event porte");
    }

}
