using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonMasterActionScript : MonoBehaviour {

    [SerializeField]
    Image image;

    [SerializeField]
    Button btn;

    [SerializeField]
    MasterController masterController;

    [SerializeField]
    AllContainerScript allContainerScript;

    [SerializeField]
    int numberContainer;

    private SpawnableObjectContainerScript go=null;

    // Use this for initialization
    void Start () {
        go = allContainerScript.getContainer(numberContainer);
        bool visible = !(go == null || go.isEmpty());
        if(btn!=null)
            btn.enabled = visible;
        if(image!=null)
            image.enabled = visible;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void selectObject()
    {
        print("selectObject");
        masterController.setObjectSelected(numberContainer, go.getFirstDisableGOIndice());
    }
}
