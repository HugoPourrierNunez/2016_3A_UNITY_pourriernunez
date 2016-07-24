using UnityEngine;
using System.Collections;
using UnityEditor;

public class FindMesh : EditorWindow
{

    private static FindMesh instance;

    public static FindMesh Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new FindMesh();
            }
            return instance;
        }
    }

    MeshScript meshScript;
    Vector3 translate;

    [MenuItem("MasterRunTools/FindMesh")]
    public static void MyMultipleSpawnableGoCreator()
    {
        FindMesh.Instance.Show();
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Mesh Script");
        meshScript = (MeshScript)EditorGUILayout.ObjectField(meshScript, typeof(MeshScript), true);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Find Mesh"))
        {
            meshScript.findChildren();
            Undo.RegisterCreatedObjectUndo(meshScript.gameObject, "FindMesh");
        }

        EditorGUILayout.BeginHorizontal();
        translate = EditorGUILayout.Vector3Field("Vector :", translate);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Make translate"))
        {
            meshScript.translate(translate);
            Undo.RegisterCreatedObjectUndo(meshScript.gameObject, "TranslateMesh");
        }

        if (GUILayout.Button("Make rotation"))
        {
            meshScript.rotate(translate);
            Undo.RegisterCreatedObjectUndo(meshScript.gameObject, "RotateMesh");
        }

        EditorGUILayout.EndVertical();
    }
}
