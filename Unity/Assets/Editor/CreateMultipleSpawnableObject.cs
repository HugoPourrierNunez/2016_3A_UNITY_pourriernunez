using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateMultipleSpawnableObject : EditorWindow
{

    private static CreateMultipleSpawnableObject instance;

    public static CreateMultipleSpawnableObject Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new CreateMultipleSpawnableObject();
            }
            return instance;
        }
    }

    int number;
    bool activated;
    SpawnableObjectContainerScript parent;
    SpawnableObjectScript gameobject;
    LocalPlayerScript localPlayerScript;
    MasterController masterController;
    RunnerListScript runnerList;

    [MenuItem("MasterRunTools/MultipleSpawnableGoCreator")]
    public static void MyMultipleSpawnableGoCreator()
    {
        CreateMultipleSpawnableObject.Instance.Show();
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
        parent = (SpawnableObjectContainerScript)EditorGUILayout.ObjectField(parent, typeof(SpawnableObjectContainerScript), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("GameObject");
        gameobject = (SpawnableObjectScript)EditorGUILayout.ObjectField(gameobject, typeof(SpawnableObjectScript), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Local Player script");
        localPlayerScript = (LocalPlayerScript)EditorGUILayout.ObjectField(localPlayerScript, typeof(LocalPlayerScript), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Master Controller");
        masterController = (MasterController)EditorGUILayout.ObjectField(masterController, typeof(MasterController), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Runner List Script");
        runnerList = (RunnerListScript)EditorGUILayout.ObjectField(runnerList, typeof(RunnerListScript), true);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Generate multiple object"))
        {
            parent.initializeChildrenList();
            for (int i = 0; i < number; i++)
            {
                var go = (SpawnableObjectScript)PrefabUtility.InstantiatePrefab(gameobject);
                go.setLocalPlayerScript(localPlayerScript);
                go.setMasterController(masterController);
                go.setIndice(i);
                go.setRunnerList(runnerList);
                go.transform.position = new Vector3(0, 0, i);
                go.gameObject.SetActive(activated);
                parent.AddChildren(go);
                Undo.RegisterCreatedObjectUndo(go, "MultipleGO");

            }
        }

        EditorGUILayout.EndVertical();
    }
}
