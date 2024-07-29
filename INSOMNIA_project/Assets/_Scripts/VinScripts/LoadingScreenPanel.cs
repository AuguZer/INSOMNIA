using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenPanel : MonoBehaviour
{
    [SerializeField] Image panelImage;
    [SerializeField] TextMeshProUGUI loadText;

    private void OnEnable()
    {
        FadeInPanel();
    }

    void FadeInPanel()
    {
        Tween panelFade = panelImage.DOFade(1f, 2f);
        panelFade.Play();

        Tween titleFade = loadText.DOFade(1f, 2f);
        titleFade.Play();
    }
}
