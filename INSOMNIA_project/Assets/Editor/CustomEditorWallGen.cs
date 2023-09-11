using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WallGeneration))]
public class CustomEditorWallGen : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WallGeneration wallGenScript = (WallGeneration)target;

        if (GUILayout.Button("Generate Wall"))
        {
            Debug.Log("Yo");
            wallGenScript.GenerateWall();

        }
    }
}
