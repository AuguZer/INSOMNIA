using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : BaseButton
{
    public override void Awake()
    {
        base.Awake();

        base.EventOnClick += OnQuitBtnClicked;
    }

    void OnQuitBtnClicked()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
