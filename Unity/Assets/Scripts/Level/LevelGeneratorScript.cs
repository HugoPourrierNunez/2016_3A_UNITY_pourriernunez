﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
/*Classe qui sert à la génération des niveaux*/
public class LevelGeneratorScript : NetworkBehaviour
{
    [SerializeField]
    int levelWidth = 9;

    [SerializeField]
    int levelLength = 70;

    [SerializeField]
    float coefForward = .5f;

    [SerializeField]
    int numberOfWay = 2;

    [SerializeField]
    ObjectContainerScript parentObstacleDestroyable;

    [SerializeField]
    ObjectContainerScript parentObstacleUndestroyable;

    [SerializeField]
    GameObject floor;

    [SerializeField]
    GameObject wallRight;

    [SerializeField]
    GameObject wallLeft;

    [SerializeField]
    GameObject endLevel;

    [SerializeField]
    int numberObjectUndestroyable=50;

    [SerializeField]
    int numberObjectDestroyable = 50;

    [SerializeField]
    MasterController masterController;

    int previousDirection;
    int nbOfForward;
    int newDirection;
    int numberOfPosition;
    int numberOfPositionTaken = 0;
    Vector3 lastPosition = new Vector3();
    List<GameObject> undestroyable;
    List<GameObject> destroyable;
    List<Vector3> positionDestroyableobstacle = new List<Vector3>();
    Vector3 randomV = new Vector3();
    private float espace = 15f;

    List<Vector3> way = new List<Vector3>();

    void Start()
    {
        undestroyable = parentObstacleUndestroyable.GetChildren();
        destroyable = parentObstacleDestroyable.GetChildren();
    }

    public void activate()
    {
        wallLeft.gameObject.SetActive(true);
        wallRight.gameObject.SetActive(true);
    }

    /*Renvoie le sol du niveau*/
    public Transform getFloor()
    {
        return floor.transform;
    }

    /*Met à jour la taille du niveau*/
    public void changeSizeLevel(float longueur, float largeur)
    {
        floor.transform.localScale = Vector3.right*(largeur / 10f)+Vector3.up* 1+Vector3.forward* (longueur / 10f + espace / 10f);
        floor.transform.localPosition = Vector3.forward* (longueur / 2f + espace / 2f);

        wallLeft.transform.localScale = Vector3.up*.2f+Vector3.forward*(longueur + espace);
        wallLeft.transform.localPosition = Vector3.right*(-largeur / 2f)+Vector3.up*.1f+Vector3.forward* (longueur / 2f + espace / 2f);

        wallRight.transform.localScale = Vector3.up*.2f+Vector3.forward*(longueur + espace);
        wallRight.transform.localPosition = Vector3.right*(largeur / 2f)+Vector3.up* .1f+Vector3.forward*(longueur / 2f + espace / 2f);

        endLevel.transform.localScale = Vector3.right * largeur + Vector3.up * 5;
        endLevel.transform.localPosition = Vector3.up*2.5f+Vector3.forward*(longueur + espace);
    }

    /*Return le conteneur qui contient tout les object destructible du level*/
    public ObjectContainerScript getDestroyableObjectContainer()
    {
        return parentObstacleDestroyable;
    }

    /*Return le conteneur qui contient tout les object indestructible du level*/
    public ObjectContainerScript getUndestroyableObjectContainer()
    {
        return parentObstacleUndestroyable;
    }

    /*Fonction qui fait toute la génération de iveau en fonction des différent paremètres*/
    public void generateLevel(int numPlayer, float longueur, float largeur, float difficulty, float numberDestroyable, float numberUndestroyable)
    {
        if (levelWidth % 2 == 0)
            levelWidth++;

        numberOfPositionTaken = 0;
        levelLength = (int)longueur;
        levelWidth = (int)largeur;
        numberOfWay = 8 - (int)difficulty;
        numberObjectDestroyable = (int)numberDestroyable;
        numberObjectUndestroyable = (int)numberUndestroyable;

        masterController.changeSizeLevel(numPlayer, longueur,largeur);

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

                if (lastPosition.z + .5 >= levelLength+espace)
                    break;
            }
        }


        undestroyable = parentObstacleUndestroyable.GetChildren();
        destroyable = parentObstacleDestroyable.GetChildren();

        positionDestroyableobstacle.Clear();

        bool find = false;

        masterController.unactiveAllObstacles(numPlayer);

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
                    masterController.activeDestroyableObstacle(randomV, i, numPlayer);
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
                    masterController.activeUndestroyableObstacle(randomV, i, numPlayer);
                    numberOfPositionTaken++;
                    way.Add(randomV);
                }
            } while (find == true && numberOfPositionTaken < numberOfPosition);

        }
    }

    /*Fonction qui désactive tout les obstacles du level*/
    public void unactiveAllObstacles()
    {
        for (int i = 0; i < destroyable.Count; i++)
        {
            destroyable[i].SetActive(false);
        }
        for (int i = 0; i < undestroyable.Count; i++)
        {
            undestroyable[i].SetActive(false);
        }
    }

    /*Active et place un obstacle destructible de rang 'nb' */
    public void activeDestroyableObstacle(Vector3 pos, int nb)
    {
        destroyable[nb].transform.localPosition = pos;
        destroyable[nb].gameObject.SetActive(true);
    }

    /*Active et place un obstacle indestructible de rang 'nb' */
    public void activeUndestroyableObstacle(Vector3 pos, int nb)
    {
        undestroyable[nb].transform.localPosition = pos;
        undestroyable[nb].gameObject.SetActive(true);
    }
}
