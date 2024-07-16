using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlit : MonoBehaviour
{
    [SerializeField] Material material;
    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetTexture("_MainTex", source);
        Graphics.Blit(source, destination, material);
    }

}
