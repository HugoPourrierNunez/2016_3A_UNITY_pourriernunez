using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ChangeCanvasScript : NetworkBehaviour {

    [SerializeField]
    Canvas mainCanvas;

    [SerializeField]
    Canvas creditsCanvas;

    [SerializeField]
    Canvas tutoCanvas;

    [SerializeField]
    NetworkManager netManager;

	// Use this for initialization
	void Start () {
        mainCanvas.enabled = true;
        creditsCanvas.enabled = false;
        tutoCanvas.enabled = false;
	}
	
	// Update is called once per frame
    public void OnBtnCreditClic()
    {
        mainCanvas.enabled = false;
        creditsCanvas.enabled = true;
    }

    public void OnBtnTutorielClic()
    {
        mainCanvas.enabled = false;
        tutoCanvas.enabled = true;
    }

    public void OnBtnPlayClic()
    {
        tutoCanvas.enabled = false;
        creditsCanvas.enabled = false;
        mainCanvas.enabled = false ;

        // Modifier condition ou autre
        if (!netManager.isNetworkActive)
        {
            netManager.StartHost();
        }
        else
        {            
            netManager.StartClient();
        }
    }

    public void OnBtnPreviousClic()
    {
        tutoCanvas.enabled = false;
        creditsCanvas.enabled = false;
        mainCanvas.enabled = true;
    }
}
