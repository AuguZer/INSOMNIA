using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : BaseButton
{
    public override void Awake()
    {
        base.Awake();

        base.EventOnClick += OnRestartBtnClicked;
    }

    void OnRestartBtnClicked()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
