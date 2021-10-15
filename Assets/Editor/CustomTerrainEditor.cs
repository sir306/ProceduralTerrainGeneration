﻿using UnityEngine;
using UnityEditor;
using EditorGUITable;

[CustomEditor(typeof(CustomTerrain))]
[CanEditMultipleObjects]

public class CustomTerrainEditor : Editor
{
    // properties ---------
    SerializedProperty randomHeightRange;

    // fold outs ---------
    bool showRandom = false;

    void OnEnable()
    {
        randomHeightRange = serializedObject.FindProperty("randomHeightRange");
    }

    public override void OnInspectorGUI()
    {
        // start of OnInspectorGUI
        serializedObject.Update();

        // Random height GUI 
        CustomTerrain terrain = (CustomTerrain) target;

        showRandom = EditorGUILayout.Foldout(showRandom, "Random");
        if (showRandom)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); // horizontal line
            GUILayout.Label("Set Heights Between Random Values", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(randomHeightRange);
            if (GUILayout.Button("Random Heights"))
            {
                terrain.RandomTerrain();
            }
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); // horizontal line
        if(GUILayout.Button("Reset Terrain"))
        {
            terrain.ResetTerrain();
        }


        // end of OnInspectorGUI
        serializedObject.ApplyModifiedProperties();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
