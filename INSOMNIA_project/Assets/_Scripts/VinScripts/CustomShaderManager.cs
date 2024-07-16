using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CustomShaderManager : MonoBehaviour
{
    [SerializeField] UniversalRendererData urpData;
    List<ScriptableRendererFeature> rendererFeatures = new List<ScriptableRendererFeature>();
    ScriptableRendererFeature fullScreenTVSnowFeature;
    [SerializeField] Material fullScreenTVSnowMat;
    
    private void Awake()
    {
        rendererFeatures = urpData.rendererFeatures;

        foreach (ScriptableRendererFeature feature in rendererFeatures)
        {
            if(feature.name == "FullScreenTVSnow")
            {
                fullScreenTVSnowFeature = feature;
            }
        }
    }

    public void SetFullScreenTVSnow(bool active)
    {
        Color color = new Color(0f, 0f, 0f, 0f);
        fullScreenTVSnowMat.SetColor("_Color", color);
        fullScreenTVSnowFeature.SetActive(active);
        fullScreenTVSnowMat.DOFade(1f, 2f);
    }

    private void OnApplicationQuit()
    {
        SetFullScreenTVSnow(false);
    }
}
