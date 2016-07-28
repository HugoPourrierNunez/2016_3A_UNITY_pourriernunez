using UnityEngine;
using System.Collections;
using UnityEditor;

public class FillCanon : EditorWindow
{

    private static FillCanon instance;

    public static FillCanon Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new FillCanon();
            }
            return instance;
        }
    }

    int number;
    bool activated;
    BallesContainerScript parent;
    BalleScript gameobject;
    CanonScript canonScript;

    [MenuItem("MasterRunTools/FillCanon")]
    public static void MyMultipleSpawnableGoCreator()
    {
        FillCanon.Instance.Show();
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
        parent = (BallesContainerScript)EditorGUILayout.ObjectField(parent, typeof(BallesContainerScript), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Canon");
        canonScript = (CanonScript)EditorGUILayout.ObjectField(canonScript, typeof(CanonScript), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("GameObject");
        gameobject = (BalleScript)EditorGUILayout.ObjectField(gameobject, typeof(BalleScript), false);
        EditorGUILayout.EndHorizontal();

        

        if (GUILayout.Button("Fill Canon"))
        {
            parent.initializeChildrenList();
            for (int i = 0; i < number; i++)
            {
                var go = (BalleScript)PrefabUtility.InstantiatePrefab(gameobject);
                go.gameObject.SetActive(activated);
                go.setCanon(canonScript);
                parent.AddChildren(go);
                Undo.RegisterCreatedObjectUndo(go, "FillCanon");

            }
        }

        EditorGUILayout.EndVertical();
    }
}
