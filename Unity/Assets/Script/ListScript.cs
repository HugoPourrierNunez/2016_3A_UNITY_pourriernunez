using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListScript : MonoBehaviour
{
    [SerializeField]
    GameObject floor1;

    [SerializeField]
    GameObject floor2;

    [SerializeField]
    int listSize;

    [SerializeField]
    GameObject prefab;

    Camera cam=null;

    private List<GameObject> listOfGO;
    private int index = 0;
    private float pos = 0;

    void Start()
    {
        listOfGO = new List<GameObject>();
        for (int i = 0; i < listSize; i++)
        {
            GameObject go = Instantiate(prefab);
            go.transform.parent = transform;
            go.transform.position = new Vector3(-(5*(i%3-1)) , 0, -5-i/3*7);
            go.transform.rotation = Quaternion.identity;
            listOfGO.Add(go);
        }
    }

    void Update()
    {
        if(cam!=null)
        {
            for (int i = 0; i < listSize; i++)
            {
                if (cam.transform.position.z < listOfGO[i].transform.position.z)
                    listOfGO[i].transform.position = new Vector3(listOfGO[i].transform.position.x, listOfGO[i].transform.position.y, listOfGO[i].transform.position.z - 42);
            }
        }
        if (cam.transform.position.z < floor1.transform.position.z)
            floor2.transform.position = new Vector3(floor2.transform.position.x, floor2.transform.position.y, floor1.transform.position.z - 100);
        if (cam.transform.position.z < floor2.transform.position.z)
            floor1.transform.position = new Vector3(floor1.transform.position.x, floor1.transform.position.y, floor2.transform.position.z - 100);
    }

    public void setCamera(Camera c)
    {
        cam = c;
    }


}
