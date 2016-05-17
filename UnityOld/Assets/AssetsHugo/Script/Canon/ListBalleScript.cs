using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListBalleScript : MonoBehaviour {

    [SerializeField]
    int listSize;

    [SerializeField]
    GameObject prefab;

    private List<GameObject> listOfGOInactive;
    private List<GameObject> listOfGOActive;


    // Use this for initialization
    void Start () {
        listOfGOInactive = new List<GameObject>();
        listOfGOActive = new List<GameObject>();
        for (int i = 0; i < listSize; i++)
        {
            GameObject balle = Instantiate(prefab);
            balle.transform.parent = transform;
            listOfGOInactive.Add(balle);
        }
    }

    public void activerBalle(Vector3 pos,Quaternion rot, Vector3 force)
    {
        for (int i = 0; i < listSize; i++)
        {
            if(!listOfGOInactive[i].active)
            {
                
                listOfGOInactive[i].active = true;
                listOfGOInactive[i].transform.position = pos;
                listOfGOInactive[i].transform.rotation = rot;
                listOfGOInactive[i].GetComponent<Rigidbody>().AddRelativeForce(force);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
