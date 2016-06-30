using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectContainerScript : MonoBehaviour {


    [SerializeField]
    List<GameObject> children;


    public List<GameObject>  GetChildren()
    {
        return children;
    }

    public void initializeChildrenList()
    {
        children = new List<GameObject>();
    }

    public void AddChildren(GameObject go)
    {
        go.transform.parent = this.transform;
        children.Add(go);
    }


    public GameObject getFirstDisableGO()
    {
        for (int i = 0; i < children.Count; i++)
            if (children[i].active)
                return children[i];
        return null;
    }

}
