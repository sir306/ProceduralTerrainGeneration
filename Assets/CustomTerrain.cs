﻿using System;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[ExecuteInEditMode]

public class CustomTerrain : MonoBehaviour
{
    public Vector2 randomHeightRange = new Vector2(0, 0.1f);
    public Terrain terrain;
    public TerrainData terrainData;

    public void RandomTerrain()
    {
        float[,] heightMap;
        heightMap = new float[terrainData.heightmapWidth, terrainData.heightmapHeight];
        for (int x = 0; x < terrainData.heightmapWidth; x++)
        {
            for (int y = 0; y < terrainData.heightmapHeight; y++)
            {
                heightMap[x, y] = UnityEngine.Random.Range(randomHeightRange.x, randomHeightRange.y);
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    void OnEnabled()
    {
        Debug.Log("Initialising Terrain Data");
        terrain = this.GetComponent<Terrain>();
        terrainData = Terrain.activeTerrain.terrainData;
    }

    void Awake()
    {
        SerializedObject tagManager = new SerializedObject(
            AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        AddTag(tagsProp, "Terrain");
        AddTag(tagsProp, "Cloud");
        AddTag(tagsProp, "Shore");

        // apply tag changes to tag database
        tagManager.ApplyModifiedProperties();

        // take this object
        this.gameObject.tag = "Terrain";
    }

    void AddTag(SerializedProperty tagsProp, string newTag)
    {
        bool found = false;
        // ensure the tag doesn't exist
        for (int i =0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(newTag)) { found = true; break; }
        }
        // add new tag
        if (!found)
        {
            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty newTagProp = tagsProp.GetArrayElementAtIndex(0);
            newTagProp.stringValue = newTag;
        }    
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