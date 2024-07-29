using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject playBtnGO;
    PlayerInput _playerInputHelper;
    EventSystem _eventSystem;

    private void OnDisable()
    {
        _playerInputHelper.onControlsChanged -= OnControlsChanged;
    }

    private void Awake()
    {
        _playerInputHelper = GetComponent<PlayerInput>();
        _eventSystem = GetComponent<EventSystem>();
        _playerInputHelper.onControlsChanged += OnControlsChanged;
    }

    void OnControlsChanged(PlayerInput obj)
    {
        if (_playerInputHelper == null) return;

        switch (_playerInputHelper.currentControlScheme)
        {
            case "Gamepad":

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if (_eventSystem.currentSelectedGameObject == null) _eventSystem.SetSelectedGameObject(playBtnGO);

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
