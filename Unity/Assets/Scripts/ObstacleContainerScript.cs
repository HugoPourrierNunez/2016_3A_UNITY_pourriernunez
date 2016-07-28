using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*Classe qui sert à contenir des LevelObstacleScript et à s'en servir pour du pulling*/
public class ObstacleContainerScript : MonoBehaviour
{

    [SerializeField]
    List<LevelObstacleScript> children;

    public bool isEmpty()
    {
        return children.Count == 0;
    }

    public List<LevelObstacleScript> GetChildren()
    {
        return children;
    }

    public void initializeChildrenList()
    {
        children = new List<LevelObstacleScript>();
    }

    public void AddChildren(LevelObstacleScript go)
    {
        go.transform.parent = this.transform;
        children.Add(go);
    }


    public LevelObstacleScript getFirstDisableGO()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (!children[i].gameObject.active)
            {
                print("find");
                children[i].gameObject.SetActive(true);
                return children[i];
            }
        }
        print("nonfind");
        return null;
    }

    public int getFirstDisableGOIndice()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (!children[i].gameObject.active)
            {
                children[i].gameObject.SetActive(true);
                return i;
            }
        }
        return -1;
    }
}

