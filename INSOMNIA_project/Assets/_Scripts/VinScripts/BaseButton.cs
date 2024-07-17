using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    [HideInInspector] public Button thisButton;
    public Action EventOnClick;

    public virtual void Awake()
    {
        thisButton = GetComponent<Button>();

        if (thisButton != null)
        {
            thisButton.onClick.AddListener(OnClickPunchScale);
        }
    }

    void OnClickPunchScale()
    {
        thisButton.interactable = false;
        Tween punchTween = gameObject.transform.DOPunchScale(new Vector3(-.1f, -.1f, 0.0f), .3f, 1, 1);
        punchTween.Play()
            .OnComplete(() =>
            {
                thisButton.interactable = true;
                EventOnClick?.Invoke();
            })
            .OnKill(() =>
            {
                thisButton.interactable = true;
            }).SetLink(gameObject);
    }
}
