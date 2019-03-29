using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class GrassTile : Tile
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/GrassTile")]
    public static void CreateGrassTile() {
        string path = EditorUtility.SaveFilePanelInProject("Save GrassTile", "GrassTile", "asset", "Save GrassTile", "Assets");
        if (path == "") {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<GrassTile>(), path);
    }
}
#endif