using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBtn : BaseButton
{
    public override void Awake()
    {
        base.Awake();

        base.EventOnClick += OnMainMenuBtnClicked;
    }

    void OnMainMenuBtnClicked()
    {
        SceneManager.LoadScene(0);
    }
}
