using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenPass : MonoBehaviour
{
    [SerializeField] private Material material;
    //[SerializeField] private Texture2D fullScreenTex;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Debug.Log("On render");
        material.SetTexture("_MainTex", src);
        //material.SetTexture("_NoiseTex", fullScreenTex);
        Graphics.Blit(src, dest, material);
    }
}
