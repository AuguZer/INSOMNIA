using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] public GameObject FirstSelectedGO;
    [SerializeField] GameObject panelToScale;
    public Action ScaleUpComplete;

    public void OnEnable()
    {
        ScaleUpPanel();
    }

    void ScaleUpPanel()
    {
        Tween tween = panelToScale.transform.DOScale(1.0f, 0.3f);
        tween.Play()
            .OnComplete(() =>
            {
                ScaleUpComplete?.Invoke();
            });
    }

    public void DisablePanel()
    {
        Tween tween = panelToScale.transform.DOScale(0.0f, 0.3f);
        tween.Play()
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
