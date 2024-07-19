using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] GameOverPanel _deathPanel;
    [SerializeField] GameOverPanel _winPanel;
    [SerializeField] PausePanel _pausePanel;
    [SerializeField] public Button continueBtn;
    PlayerInput playerInputHelper;

    private void OnDisable()
    {
        playerInputHelper.onControlsChanged -= OnControlsChanged;
    }

    private void Awake()
    {
        playerInputHelper = GetComponent<PlayerInput>();
        playerInputHelper.onControlsChanged += OnControlsChanged;
        _deathPanel.FadeInComplete += () => _eventSystem.SetSelectedGameObject(_deathPanel.FirstSelectedGO);
        _winPanel.FadeInComplete += () => _eventSystem.SetSelectedGameObject(_winPanel.FirstSelectedGO);
        _pausePanel.ScaleUpComplete += (() =>
        {
            if (playerInputHelper == null) return;

            switch (playerInputHelper.currentControlScheme)
            {
                case "Gamepad":

                    if (_eventSystem.currentSelectedGameObject == null) _eventSystem.SetSelectedGameObject(_pausePanel.FirstSelectedGO);

                    break;
                case "Keyboard&Mouse":

                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    _eventSystem.SetSelectedGameObject(null);

                    break;
                default:
                    break;
            }
        });
    }

    public void ActiveWinPanel()
    {
        _winPanel.gameObject.SetActive(true);
    }

    public void ActiveDeathPanel()
    {
        _deathPanel.gameObject.SetActive(true);
    }

    public void ActivePausePanel()
    {
        _pausePanel.gameObject.SetActive(true);
    }

    public void DeactivePausePanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pausePanel.DisablePanel();
    }

    void OnControlsChanged(PlayerInput obj)
    {
        if (playerInputHelper == null) return;

        switch (playerInputHelper.currentControlScheme)
        {
            case "Gamepad":

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if (_eventSystem.currentSelectedGameObject == null) _eventSystem.SetSelectedGameObject(_pausePanel.FirstSelectedGO);

                break;
            case "Keyboard&Mouse":

                _eventSystem.SetSelectedGameObject(null);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                break;
            default:
                break;
        }
    }
}
