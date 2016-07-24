using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateMultipleObstacle : EditorWindow
{

    private static CreateMultipleObstacle instance;

    public static CreateMultipleObstacle Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new CreateMultipleObstacle();
            }
            return instance;
        }
    }

    int number;
    bool activated;
    ObstacleContainerScript parent;
    LevelObstacleScript obstacle;
    LevelGeneratorScript level;

    [MenuItem("MasterRunTools/MultipleObstacleCreator")]
    public static void MyMultipleGoCreator()
    {
        CreateMultipleObstacle.Instance.Show();
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
        parent = (ObstacleContainerScript)EditorGUILayout.ObjectField(parent, typeof(ObstacleContainerScript), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Obstacle");
        obstacle = (LevelObstacleScript)EditorGUILayout.ObjectField(obstacle, typeof(LevelObstacleScript), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level");
        level = (LevelGeneratorScript)EditorGUILayout.ObjectField(level, typeof(LevelGeneratorScript), true);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Generate multiple object"))
        {
            parent.initializeChildrenList();
            for (int i = 0; i < number; i++)
            {
                var obs = (LevelObstacleScript)PrefabUtility.InstantiatePrefab(obstacle);
                obs.gameObject.transform.position = new Vector3(0, 0, i);
                obs.gameObject.SetActive(activated);
                obs.setIndice(i);
                obs.setLevel(level);
                parent.AddChildren(obs);
                Undo.RegisterCreatedObjectUndo(obs.gameObject, "MultipleObstacle");

            }
        }

        EditorGUILayout.EndVertical();
    }
}
