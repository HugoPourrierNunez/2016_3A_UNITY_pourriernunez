﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MasterUIManagerScript : MonoBehaviour {

    [SerializeField]
    Transform panelHaut;

    [SerializeField]
    Transform panelBas;

    [SerializeField]
    Transform panelBasPrincipal;

    [SerializeField]
    Transform panelBasMonstre;

    [SerializeField]
    Transform panelBasObject;

    [SerializeField]
    Transform panelBasSort;


    public void hidePanelBasMonstre()
    {
        panelBasMonstre.gameObject.SetActive(false);
        panelBasPrincipal.gameObject.SetActive(true);
    }

    public void showPanelBasMonstre()
    {
        panelBasMonstre.gameObject.SetActive(true);
        panelBasPrincipal.gameObject.SetActive(false);
    }

    public void hidePanelBasObject()
    {
        panelBasObject.gameObject.SetActive(false);
        panelBasPrincipal.gameObject.SetActive(true);
    }

    public void showPanelBasObject()
    {
        panelBasObject.gameObject.SetActive(true);
        panelBasPrincipal.gameObject.SetActive(false);
    }

    public void hidePanelBasSort()
    {
        panelBasSort.gameObject.SetActive(false);
        panelBasPrincipal.gameObject.SetActive(true);
    }

    public void showPanelBasSort()
    {
        panelBasSort.gameObject.SetActive(true);
        panelBasPrincipal.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}