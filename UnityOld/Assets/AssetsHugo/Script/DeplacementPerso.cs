using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //avant
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            //arriere
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //droite
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //gauche
        }
    }
}
