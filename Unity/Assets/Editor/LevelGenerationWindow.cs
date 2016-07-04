using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class LevelGenerationWindow : EditorWindow
{

    private static LevelGenerationWindow instance;

    public static LevelGenerationWindow Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new LevelGenerationWindow();
            }
            return instance;
        }
    }

    int levelWidth=9;
    int levelLength=70;
    float coefForward=.5f;
    ObjectContainerScript parentObstacleDestroyable;
    ObjectContainerScript parentObstacleUndestroyable;
    GameObject floor;
    GameObject wallRight;
    GameObject wallLeft;
    GameObject endLevel;
    int numberObjectUndestroyable;
    int numberObjectDestroyable;
    int numberOfWay = 2;
    int espace = 15;
    int previousDirection;
    int nbOfForward;
    int newDirection;
    int numberOfPosition;
    int numberOfPositionTaken = 0; Vector3 randomV = new Vector3();
    List<Vector3> way = new List<Vector3>();
    Vector3 lastPosition = new Vector3();
    List<GameObject> undestroyable;
    List<GameObject> destroyable;
    List<Vector3> positionDestroyableobstacle = new List<Vector3>();

    [MenuItem("MasterRunTools/LevelGenerator")]
    public static void MyLevelGenerationWindow()
    {
        LevelGenerationWindow.Instance.Show();
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Floor");
        floor = (GameObject) EditorGUILayout.ObjectField(floor, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Wall right");
        wallRight = (GameObject)EditorGUILayout.ObjectField(wallRight, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Wall left");
        wallLeft = (GameObject)EditorGUILayout.ObjectField(wallLeft, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("End level");
        endLevel = (GameObject)EditorGUILayout.ObjectField(endLevel, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level length");
        levelLength = EditorGUILayout.IntField(levelLength);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level width");
        levelWidth = EditorGUILayout.IntField(levelWidth);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number of way");
        numberOfWay = EditorGUILayout.IntField(numberOfWay);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Parent obstacle destroyable");
        parentObstacleDestroyable = (ObjectContainerScript)EditorGUILayout.ObjectField(parentObstacleDestroyable, typeof(ObjectContainerScript), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number obstacle destroyable");
        numberObjectDestroyable = EditorGUILayout.IntField(numberObjectDestroyable);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Parent obstacle undestroyable");
        parentObstacleUndestroyable = (ObjectContainerScript)EditorGUILayout.ObjectField(parentObstacleUndestroyable, typeof(ObjectContainerScript), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number obstacle undestroyable");
        numberObjectUndestroyable = EditorGUILayout.IntField(numberObjectUndestroyable);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Generate Level"))
        {
            if (levelWidth % 2 == 0)
                levelWidth++;

            floor.transform.localScale = Vector3.right * (levelWidth / 10f) + Vector3.up * 1 + Vector3.forward * (levelWidth / 10f + espace / 10f);
            floor.transform.localPosition = Vector3.forward * (levelLength / 2f + espace / 2f);

            wallLeft.transform.localScale = Vector3.up * .2f + Vector3.forward * (levelLength + espace);
            wallLeft.transform.localPosition = Vector3.right * (-levelWidth / 2f) + Vector3.up * .1f + Vector3.forward * (levelLength / 2f + espace / 2f);

            wallRight.transform.localScale = Vector3.up * .2f + Vector3.forward * (levelLength + espace);
            wallRight.transform.localPosition = Vector3.right * (levelWidth / 2f) + Vector3.up * .1f + Vector3.forward * (levelLength / 2f + espace / 2f);

            endLevel.transform.localScale = Vector3.right * levelWidth + Vector3.up * 5;
            endLevel.transform.localPosition = Vector3.up * 2.5f + Vector3.forward * (levelLength + espace);

            // 0=gauche, 1=droite, 2=devant

            numberOfPosition = levelLength * levelWidth;
            way.Clear();

            for (int i = 0; i < numberOfWay; i++)
            {
                lastPosition.Set(0, .5f, 0.5f);
                previousDirection = 2;
                nbOfForward = 0;
                while (true)
                {
                    do
                    {
                        float nb = Random.Range(0f, 1f);
                        if (nb > coefForward && nbOfForward >= 2)
                        {
                            if (lastPosition.x <= -(levelWidth / 2))
                                newDirection = 1;
                            else if (lastPosition.x >= levelWidth / 2)
                                newDirection = 0;
                            else
                                newDirection = Random.Range(0, 2);
                        }
                        else
                        {
                            newDirection = 2;
                        }
                    } while (newDirection == previousDirection && newDirection != 2);
                    if (newDirection == 2)
                        nbOfForward++;
                    else nbOfForward = 0;

                    if (newDirection == 0)
                    {
                        lastPosition += Vector3.left;
                    }
                    else if (newDirection == 1)
                    {
                        lastPosition += Vector3.right;
                    }
                    else if (newDirection == 2)
                    {
                        lastPosition += Vector3.forward;
                    }
                    bool alreadyIn = false;
                    for (int j = 0; j < way.Count; j++)
                    {
                        Vector3 v = way[j];
                        if (v.x == lastPosition.x && v.z == lastPosition.z)
                        {
                            alreadyIn = true;
                            break;
                        }
                    }
                    if (!alreadyIn)
                        way.Add(lastPosition);

                    previousDirection = newDirection;

                    if (lastPosition.z + .5 >= levelLength + espace)
                        break;
                }
            }


            undestroyable = parentObstacleUndestroyable.GetChildren();
            destroyable = parentObstacleDestroyable.GetChildren();

            positionDestroyableobstacle.Clear();

            bool find = false;

            for (int i = 0; i < destroyable.Count; i++)
                destroyable[i].gameObject.SetActive(false);

            for (int i = 0; i < undestroyable.Count; i++)
                undestroyable[i].gameObject.SetActive(false);

            for (int i = 0; i < destroyable.Count && i < numberObjectDestroyable && numberOfPositionTaken < numberOfPosition; i++)
            {
                do
                {
                    find = false;
                    Vector3 randomV = Vector3.up * .5f;
                    randomV.x = Random.Range(-levelWidth / 2, levelWidth / 2 + 1);
                    randomV.z = Random.Range(0, levelLength) + espace;
                    randomV.z += .5f;
                    for (int j = 0; j < positionDestroyableobstacle.Count; j++)
                    {
                        Vector3 v = positionDestroyableobstacle[j];
                        if (v.x == randomV.x && v.z == randomV.z)
                        {
                            find = true;
                            break;
                        }
                    }
                    if (!find)
                    {
                        destroyable[i].SetActive(true);
                        numberOfPositionTaken++;
                        positionDestroyableobstacle.Add(randomV);
                    }
                } while (find == true);

            }
            way.AddRange(positionDestroyableobstacle);

            if (numberObjectDestroyable + numberObjectUndestroyable > levelLength * levelWidth)
            {
                numberObjectUndestroyable -= (levelLength * levelWidth) - numberObjectDestroyable;
            }

            numberOfPositionTaken = way.Count;
            for (int i = 0; i < undestroyable.Count && i < numberObjectUndestroyable && numberOfPositionTaken < numberOfPosition; i++)
            {
                do
                {
                    find = false;
                    randomV.Set(Random.Range(-levelWidth / 2, levelWidth / 2 + 1),
                                .5f,
                                Random.Range(0, levelLength) + .5f + espace);

                    for (int j = 0; j < way.Count; j++)
                    {
                        Vector3 v = way[j];
                        if (v.x == randomV.x && v.z == randomV.z)
                        {
                            find = true;
                            break;
                        }
                    }
                    if (!find)
                    {
                        undestroyable[i].SetActive(true);
                        numberOfPositionTaken++;
                        way.Add(randomV);
                    }
                } while (find == true && numberOfPositionTaken < numberOfPosition);

            }
        }

        EditorGUILayout.EndVertical();
    }
}