using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TGMap))]
public class TGMapInspector : Editor {
    public override void OnInspectorGUI() {
        // Create a regenerate button under the TileMap object
        DrawDefaultInspector();
        if(GUILayout.Button("Regenerate")) {
            TGMap tileMap = (TGMap)target;
            tileMap.BuildMesh();
        }
    }
}
