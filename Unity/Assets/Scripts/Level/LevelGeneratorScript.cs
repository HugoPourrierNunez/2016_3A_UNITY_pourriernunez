using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
/*Classe qui sert à la génération des niveaux*/
public class LevelGeneratorScript : NetworkBehaviour
{
    public static readonly int MAX_LENGTH = 500;
    public static readonly int MIN_LENGTH = 70;

    public static readonly int MAX_WIDTH = 15;
    public static readonly int MIN_WIDTH = 7;

    public static readonly int MAX_DIFFICULTY = 5;
    public static readonly int MIN_DIFFICULTY = 1;

    public static readonly int MAX_DESTROYABLEOBJECT = 500;
    public static readonly int MIN_DESTROYABLEOBJECT = 0;

    public static readonly int MAX_UNDESTROYABLEOBJECT = 500;
    public static readonly int MIN_UNDESTROYABLEOBJECT = 0;

    [SerializeField]
    int levelWidth = 9;

    [SerializeField]
    int levelLength = 70;

    [SerializeField]
    float coefForward = .5f;

    [SerializeField]
    int numberOfWay = 2;

    [SerializeField]
    ObstacleContainerScript parentObstacleDestroyable;

    [SerializeField]
    ObstacleContainerScript parentObstacleUndestroyable;

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
    GameObject runnerContainer;

    [SerializeField]
    MasterController masterController;

    [SerializeField]
    RunnerController runnerController;

    int previousDirection;
    int nbOfForward;
    int newDirection;
    int numberOfPosition;
    int numberOfPositionTaken = 0;
    Vector3 lastPosition = new Vector3();
    List<LevelObstacleScript> undestroyable;
    List<LevelObstacleScript> destroyable;
    List<Vector3> positionDestroyableobstacle = new List<Vector3>();
    int[] grid = new int[MAX_WIDTH * MAX_LENGTH]; 
    Vector3 randomV = new Vector3();
    private float espace = 15f;

    List<Vector3> way = new List<Vector3>();

    void Start()
    {
        undestroyable = parentObstacleUndestroyable.GetChildren();
        destroyable = parentObstacleDestroyable.GetChildren();
    }

    public RunnerController getRunnerController()
    {
        return this.runnerController;
    }

    public void activate()
    {
        wallLeft.gameObject.SetActive(true);
        wallRight.gameObject.SetActive(true);
    }

    public int getIndexDestroyableObstacle(GameObject go)
    {
        for(int i=0;i<destroyable.Count;i++)
        {
            if (go == destroyable[i].gameObject)
                return i;
        }
        return -1;
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
    public ObstacleContainerScript getDestroyableObjectContainer()
    {
        return parentObstacleDestroyable;
    }

    /*Return le conteneur qui contient tout les object indestructible du level*/
    public ObstacleContainerScript getUndestroyableObjectContainer()
    {
        return parentObstacleUndestroyable;
    }

    /*Fonction qui fait toute la génération de iveau en fonction des différent paremètres*/
    public void generateLevel(int numPlayer, float longueur, float largeur, float difficulty, float numberDestroyable, float numberUndestroyable)
    {
        for(int i=0;i<grid.Length;i++)
        {
            grid[i] = 0;
        }

        if (largeur % 2 == 0)
            largeur++;

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
                Vector3 randomV = Vector3.zero;
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
                    randomV.y = destroyable[i].getY();
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
                            0,
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
                    randomV.y = undestroyable[i].getY();
                    masterController.activeUndestroyableObstacle(randomV, i, numPlayer);
                    numberOfPositionTaken++;
                    way.Add(randomV);
                }
            } while (find == true && numberOfPositionTaken < numberOfPosition);

        }
    }

    public void setPositionOccuped(Vector3 pos, bool localPosition)
    {
        if(localPosition)
        {
            grid[((int)(pos.z - .5f) * levelWidth + (int)pos.x + (levelWidth - 1) / 2)] = int.MaxValue;
        }
        else
        {
            grid[((int)(pos.z - .5f) * levelWidth + (int)(pos.x - runnerContainer.transform.position.x+(levelWidth - 1)/2))] = int.MaxValue;
        }
    }

    public void setPositionNotOccuped(Vector3 pos,bool localPosition)
    {
        if (localPosition)
        {
            grid[((int)(pos.z - .5f) * levelWidth + (int)pos.x + (levelWidth - 1) / 2)] = int.MaxValue;
        }
        else
        {
            grid[((int)(pos.z - .5f) * levelWidth + (int)(pos.x - runnerContainer.transform.position.x + (levelWidth - 1) / 2))] = int.MaxValue;
        }
    }

    /*Fonction qui désactive tout les obstacles du level*/
    public void unactiveAllObstacles()
    {
        for (int i = 0; i < destroyable.Count; i++)
        {
            destroyable[i].gameObject.SetActive(false);
            destroyable[i].transform.localPosition = -Vector3.one;
            destroyable[i].setTransparent(false);
        }
        for (int i = 0; i < undestroyable.Count; i++)
        {
            undestroyable[i].gameObject.SetActive(false);
            undestroyable[i].transform.localPosition = -Vector3.one;
            destroyable[i].setTransparent(false);
        }
    }

    public bool isPositionOccuped(Vector3 pos, bool localPosition)
    {
        if (localPosition)
        {
            return (grid[((int)(pos.z - .5f) * levelWidth + (int)pos.x + (levelWidth - 1) / 2)] != 0);
        }
        else
        {
            return (grid[((int)(pos.z - .5f) * levelWidth + (int)(pos.x - runnerContainer.transform.position.x + (levelWidth - 1) / 2))] != 0);
        }
    }

    public void setAllObstacleTransparent(bool isTransparent)
    {
        for(int i=0;i<destroyable.Count;i++)
        {
            LevelObstacleScript obs = destroyable[i];
            if (obs.gameObject.active)
            {
                obs.setTransparent(isTransparent);
            }
        }

        for (int i = 0; i < undestroyable.Count; i++)
        {
            LevelObstacleScript obs = undestroyable[i];
            if (obs.gameObject.active)
            {
                obs.setTransparent(isTransparent);
            }
        }
    }

    /*Active et place un obstacle destructible de rang 'nb' */
    public void activeDestroyableObstacle(Vector3 pos, int nb)
    {
        setPositionOccuped(pos,true);
        destroyable[nb].transform.localPosition = pos;
        destroyable[nb].gameObject.SetActive(true);
    }

    /*Active et place un obstacle indestructible de rang 'nb' */
    public void activeUndestroyableObstacle(Vector3 pos, int nb)
    {
        setPositionOccuped(pos,true);
        undestroyable[nb].transform.localPosition = pos;
        undestroyable[nb].gameObject.SetActive(true);
    }

    public void DesactiveDestroyableObstacle(int nb)
    {
        setPositionNotOccuped(destroyable[nb].gameObject.transform.localPosition,true);
        destroyable[nb].gameObject.SetActive(false);
        destroyable[nb].setTransparent(false);
    }

    public void DesactiveUndestroyableObstacle(int nb)
    {
        setPositionNotOccuped(undestroyable[nb].gameObject.transform.localPosition,true);
        undestroyable[nb].gameObject.SetActive(false);
        destroyable[nb].setTransparent(false);
    }

    public int getIndiceObstacle(Vector3 pos, out bool isDestroyable)
    {
        for (int i = 0; i < destroyable.Count; i++)
        {
            if(destroyable[i].transform.localPosition==pos)
            {
                isDestroyable = true;
                return i;
            }
        }
        for (int i = 0; i < undestroyable.Count; i++)
        {
            if (undestroyable[i].transform.localPosition == pos)
            {
                isDestroyable = false;
                return i;
            }
        }
        isDestroyable = false;
        return -1;
    }
}
