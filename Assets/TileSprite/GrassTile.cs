using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class GrassTiles : Tile
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/GrassTile")]
    public static void CreateGrassTiles() {
        string path = EditorUtility.SaveFilePanelInProject("Save GrassTile", "GrassTile", "asset", "Save GrassTile", "Assets");
        if (path == "") {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<GrassTiles>(), path);
    }
}
#endif