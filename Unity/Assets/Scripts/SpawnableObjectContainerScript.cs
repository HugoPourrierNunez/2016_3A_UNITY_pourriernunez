﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*Classe qui sert à contenir des spawnableObjectScript et à s'en servir pour du pulling*/
public class SpawnableObjectContainerScript : MonoBehaviour {

    [SerializeField]
    List<SpawnableObjectScript> children;

    public bool isEmpty()
    {
        return children.Count == 0;
    }

    public List<SpawnableObjectScript> GetChildren()
    {
        return children;
    }

    public void initializeChildrenList()
    {
        children = new List<SpawnableObjectScript>();
    }

    public void AddChildren(SpawnableObjectScript go)
    {
        go.transform.parent = this.transform;
        children.Add(go);
    }

    public void DesactiveAll()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Desactive();
        }

    }


    public SpawnableObjectScript getFirstDisableGO()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (!children[i].gameObject.active)
            {
                //print("find");
                children[i].gameObject.SetActive(true);
                return children[i];
            }
        }
        //print("nonfind");
        return null;
    }

    public int getFirstDisableGOIndice()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (!children[i].gameObject.active)
            {
                //print("find");
                children[i].gameObject.SetActive(true);
                return i;
            }
        }
        //print("nonfind");
        return -1;
    }
}
