using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TileManager))]
public class EditorButton : Editor
{
    public override void OnInspectorGUI() //2
    {
        base.DrawDefaultInspector();


        TileManager manage = (TileManager)target; //1

        GUILayout.Space(20f); //2
        GUILayout.Label("Custom Editor Elements", EditorStyles.boldLabel); //3
        

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate Tile Map")) //8
        {
            manage.GenerateTileMap();
            Debug.Log("TileMap Generated");
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Erase Tile Map")) //8
        {
            manage.EraseTileMap();
            Debug.Log("TileMap Deleted");
        }

        GUILayout.EndHorizontal();

    }
}
