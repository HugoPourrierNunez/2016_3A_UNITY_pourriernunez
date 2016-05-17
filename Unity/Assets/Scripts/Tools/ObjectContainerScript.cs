using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectContainerScript : MonoBehaviour {


    [SerializeField]
    List<GameObject> children;

    void Start()
    {

    }

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

}
