using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PullingProjectileScript : NetworkBehaviour {


    [SerializeField]
    Renderer myRenderer;

    [SerializeField]
    GameObject myGameObject;
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (!myRenderer.isVisible)
        {
            myRenderer.enabled = false;
        }
        {
            myRenderer.enabled = true;
        }
    }
}
