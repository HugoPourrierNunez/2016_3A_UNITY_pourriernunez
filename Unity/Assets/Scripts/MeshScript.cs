using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshScript : MonoBehaviour {

    [SerializeField]
    List<MeshRenderer> children;

    public void findChildren()
    {
        for(int i=0;i<transform.GetChildCount();i++)
        {
            children.Add(transform.GetChild(i).GetComponent<MeshRenderer>());
        }

    }

    public void setMaterial(Material mat)
    {
        for(int i=0;i<children.Count;i++)
        {
            children[i].material = mat;
        }
    }

    public void translate(Vector3 trans)
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].transform.Translate(trans, Space.World);
        }
    }

    public void rotate(Vector3 rot)
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].transform.Rotate(rot, Space.Self);
        }
    }
}
