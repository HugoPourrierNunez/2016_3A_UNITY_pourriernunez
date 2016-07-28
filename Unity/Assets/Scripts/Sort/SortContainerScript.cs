using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*Classe qui sert à référencer et à accéder à tout les types de sorts*/
public class SortContainerScript : MonoBehaviour
{
    [SerializeField]
    List<AbstractSortScript> children;

    void Start()
    {
        for(int i=0;i<children.Count;i++)
        {
            children[i].setNumber(i);
        }
    }


    public List<AbstractSortScript> GetChildren()
    {
        return children;
    }

    public void initializeChildrenList()
    {
        children = new List<AbstractSortScript>();
    }

    public void AddChildren(AbstractSortScript go)
    {
        go.transform.parent = this.transform;
        children.Add(go);
    }


    public AbstractSortScript get(int i)
    {
        if (i < 0 || i >= children.Count)
            return null;
        return children[i];

    }
}
