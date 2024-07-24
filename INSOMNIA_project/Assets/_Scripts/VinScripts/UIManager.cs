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
    GameObject currentGOToSelect;

    [SerializeField] PlayerInputManager playerInputManager;
    [SerializeField] PlayerCam playerCam;
    [SerializeField] EnemyStateManager enemyStateManager;

    public bool gamePaused;

    private void OnDisable()
    {
        playerInputHelper.onControlsChanged -= OnControlsChanged;
    }

    private void Awake()
    {
        playerInputHelper = GetComponent<PlayerInput>();
        playerInputHelper.onControlsChanged += OnControlsChanged;
        _deathPanel.FadeInComplete += (() =>
        {
            if (playerInputHelper == null) return;

            switch (playerInputHelper.currentControlScheme)
            {
                case "Gamepad":

                    if (_eventSystem.currentSelectedGameObject == null) _eventSystem.SetSelectedGameObject(_winPanel.FirstSelectedGO);

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

        _winPanel.FadeInComplete += (() =>
        {
            if (playerInputHelper == null) return;

            switch (playerInputHelper.currentControlScheme)
            {
                case "Gamepad":

                    if (_eventSystem.currentSelectedGameObject == null) _eventSystem.SetSelectedGameObject(_deathPanel.FirstSelectedGO);

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
        currentGOToSelect = _winPanel.FirstSelectedGO;
        _winPanel.gameObject.SetActive(true);
    }

    public void ActiveDeathPanel()
    {
        currentGOToSelect = _deathPanel.FirstSelectedGO;
        _deathPanel.gameObject.SetActive(true);
    }

    public void ActivePausePanel()
    {
        currentGOToSelect = _pausePanel.FirstSelectedGO;
        _pausePanel.gameObject.SetActive(true);
        PauseGame();
    }
    private void PauseGame()
    {
        gamePaused = true;
        playerCam.enabled = false;
        playerInputManager.enabled = false;
    }

    public void DeactivePausePanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pausePanel.DisablePanel();
        ResumeGame();
    }

    private void ResumeGame()
    {
        Debug.Log("Resume");
        playerCam.enabled = true;
        playerInputManager.enabled = true;
        gamePaused = false;
    }

    void OnControlsChanged(PlayerInput obj)
    {
        if (playerInputHelper == null) return;

        switch (playerInputHelper.currentControlScheme)
        {
            case "Gamepad":

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if (_eventSystem.currentSelectedGameObject == null) _eventSystem.SetSelectedGameObject(currentGOToSelect);

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
