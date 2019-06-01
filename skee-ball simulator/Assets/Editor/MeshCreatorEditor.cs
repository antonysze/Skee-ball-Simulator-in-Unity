using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshCreator),true)]
public class MeshCreatorEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        MeshCreator meshCreator = (MeshCreator)target;

        if (GUILayout.Button("Build Mesh")) {
            meshCreator.updateMesh();
        }
    }
}