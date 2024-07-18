using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] GameOverPanel _deathPanel;
    [SerializeField] GameOverPanel _winPanel;
    [SerializeField] PausePanel _pausePanel;

    private void Awake()
    {
        _deathPanel.FadeInComplete += () => _eventSystem.SetSelectedGameObject(_deathPanel.FirstSelectedGO);
        _winPanel.FadeInComplete += () => _eventSystem.SetSelectedGameObject(_winPanel.FirstSelectedGO);
        _pausePanel.ScaleUpComplete += () => _eventSystem.SetSelectedGameObject(_pausePanel.FirstSelectedGO);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ActiveWinPanel();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ActiveDeathPanel();
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            ActivePausePanel();
        }
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
}
