using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeCanvasScript : MonoBehaviour {

    [SerializeField]
    Canvas mainCanvas;

    [SerializeField]
    Canvas creditsCanvas;

    [SerializeField]
    Canvas tutoCanvas;

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

    public void OnBtnPreviousClic()
    {
        tutoCanvas.enabled = false;
        creditsCanvas.enabled = false;
        mainCanvas.enabled = true;
    }
}
