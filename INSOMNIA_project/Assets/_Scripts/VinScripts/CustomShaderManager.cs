using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CustomShaderManager : MonoBehaviour
{
    [SerializeField] UniversalRendererData urpData;
    List<ScriptableRendererFeature> rendererFeatures = new List<ScriptableRendererFeature>();
    ScriptableRendererFeature fullScreenTVSnowFeature;

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
        fullScreenTVSnowFeature.SetActive(active);
    }

    private void OnApplicationQuit()
    {
        SetFullScreenTVSnow(false);
    }
}
