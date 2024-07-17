using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : BaseButton
{
    public override void Awake()
    {
        base.Awake();

        base.EventOnClick += OnPlayBtnClicked;
    }

    void OnPlayBtnClicked()
    {
        SceneManager.LoadScene(1);
    }
}
