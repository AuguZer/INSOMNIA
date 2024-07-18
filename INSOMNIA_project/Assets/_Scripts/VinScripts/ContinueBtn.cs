using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContinueBtn : BaseButton
{
    [SerializeField] UnityEvent onClickEvent;
    
    public override void Awake()
    {
        base.Awake();

        base.EventOnClick += () => onClickEvent?.Invoke();
    }
}
