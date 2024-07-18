using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] public GameObject FirstSelectedGO;
    [SerializeField] Image panelImage;
    [SerializeField] TextMeshProUGUI[] texts;
    public Action FadeInComplete;

    private void OnEnable()
    {
        FadeInPanel();
    }

    void FadeInPanel()
    {
        Tween panelFade = panelImage.DOFade(1f, 2f);
        panelFade.Play()
            .OnComplete(() =>
            {
                Tween titleFade = texts[0].DOFade(1f, 2f);
                titleFade.Play()
                    .OnComplete(() =>
                {
                    texts[1].DOFade(1f, 1f);
                    texts[2].DOFade(1f, 1f);
                    texts[3].DOFade(1f, 1f);
                    FadeInComplete?.Invoke();
                });

            });
    }

    public void DisablePanel()
    {
        FadeOutPanel();
    }

    void FadeOutPanel()
    {
        panelImage.DOFade(0f, .1f);
        texts[0].DOFade(0f, .1f);
        texts[1].DOFade(0f, .1f);
        texts[2].DOFade(0f, .1f);
        texts[3].DOFade(0f, .1f);
    }
}
