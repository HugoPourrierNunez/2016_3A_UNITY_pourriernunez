using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateGameObjectWindow : EditorWindow
{

    private static CreateGameObjectWindow instance;

    public static CreateGameObjectWindow Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new CreateGameObjectWindow();
            }
            return instance;
        }
    }

    int number;
    bool activated;
    ObjectContainerScript parent;
    GameObject gameobject;

    [MenuItem("MasterRunTools/MultipleGoCreator")]
    public static void MyPultipleGoCreator()
    {
        CreateGameObjectWindow.Instance.Show();
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number");
        number = EditorGUILayout.IntField(number);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Activated");
        activated = EditorGUILayout.Toggle(activated);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Parent");
        parent = (ObjectContainerScript)EditorGUILayout.ObjectField(parent, typeof(ObjectContainerScript), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("GameObject");
        gameobject = (GameObject)EditorGUILayout.ObjectField(gameobject, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Generate multiple object"))
        {
            parent.initializeChildrenList();
            for (int i = 0; i < number; i++)
            {
                var go = (GameObject)PrefabUtility.InstantiatePrefab(gameobject);
                go.transform.position = new Vector3(0, 0, i);
                go.SetActive(activated);
                parent.AddChildren(go);
                Undo.RegisterCreatedObjectUndo(go, "MultipleGO");

            }
        }

        EditorGUILayout.EndVertical();
    }
}
