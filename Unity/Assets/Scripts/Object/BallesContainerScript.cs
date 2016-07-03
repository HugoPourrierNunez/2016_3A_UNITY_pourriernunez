using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallesContainerScript : MonoBehaviour
{
    [SerializeField]
    List<BalleScript> children;


    public List<BalleScript> GetChildren()
    {
        return children;
    }

    public void initializeChildrenList()
    {
        children = new List<BalleScript>();
    }

    public void AddChildren(BalleScript go)
    {
        go.transform.parent = this.transform;
        children.Add(go);
    }


    public BalleScript getFirstDisableGO()
    {
        for (int i = 0; i < children.Count; i++)
            if (!children[i].gameObject.active)
                return children[i];
        return null;
    }

}
