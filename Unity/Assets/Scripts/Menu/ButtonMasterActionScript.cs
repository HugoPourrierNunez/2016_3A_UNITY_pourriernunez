using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonMasterActionScript : MonoBehaviour {

    [SerializeField]
    GameObject go;

    [SerializeField]
    Image image;

    [SerializeField]
    Button btn;

    // Use this for initialization
    void Start () {

        bool visible = (go != null);
        if(btn!=null)
            btn.enabled = visible;
        if(image!=null)
            image.enabled = visible;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
