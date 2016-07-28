using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/*Classe qui gère la navigation dans l'UI du master*/
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

    [SerializeField]
    HBarScript runnerPvBar;

    [SerializeField]
    HBarScript runner2PvBar;

    [SerializeField]
    GameObject runner2PVPanel;

    [SerializeField]
    Image backgroundRunner1;

    [SerializeField]
    Image backgroundRunner2;

    [SerializeField]
    HBarScript masterManaBar;

    [SerializeField]
    List<ButtonMasterSortScript> buttonsSort;

    [SerializeField]
    List<ButtonMasterActionScript> buttonsObject;

    [SerializeField]
    List<ButtonMasterActionScript> buttonsMonster;

    private Color colorFocused = new Color(1, 1, 1, 1);
    private Color colorUnfocused = new Color(1, 1, 1, .39f);

    private ButtonMasterActionScript lastSelectedAction=null;
    private ButtonMasterSortScript lastSelectedSort = null;

    /*Renvoie la barre de progression de mana du master*/
    public HBarScript getMasterManaBar()
    {
        return masterManaBar;
    }

    /*Renvoie la barre de progression des pv du runner1*/
    public HBarScript getRunnerPvBar()
    {
        return runnerPvBar;
    }

    /*Renvoie la barre de progression des pv du runner2*/
    public HBarScript getRunner2PvBar()
    {
        return runner2PvBar;
    }

    public void activeRunner2PVBar()
    {
        runner2PVPanel.SetActive(true);
    }

    public void setRunnerFocused(int i)
    {
        if (i == 0)
        {
            backgroundRunner1.color = colorFocused;
            backgroundRunner2.color = colorUnfocused;
        }
        else
        {
            backgroundRunner1.color = colorUnfocused;
            backgroundRunner2.color = colorFocused;
        }
            
    }

    public void keyPressed(KeyCode key)
    {
        if (key==KeyCode.Alpha1)
        {
            if (panelBasPrincipal.gameObject.active)
                showPanelBasMonstre();
            else if (panelBasSort.gameObject.active)
            {
                buttonsSort[0].selectSort();
                lastSelectedSort = buttonsSort[0];
                lastSelectedAction = null;
            }
            else if (panelBasMonstre.gameObject.active)
            {
                buttonsMonster[0].selectObject();
                lastSelectedSort = null;
                lastSelectedAction = buttonsMonster[0];
            }
            else if (panelBasObject.gameObject.active)
            {
                buttonsObject[0].selectObject();
                lastSelectedSort = null;
                lastSelectedAction = buttonsObject[0];
            }
        }
        else if (key == KeyCode.Alpha2)
        {
            if (panelBasPrincipal.gameObject.active)
                showPanelBasObject();
            else if (panelBasSort.gameObject.active)
            { 
                buttonsSort[1].selectSort();
                lastSelectedSort = buttonsSort[1];
                lastSelectedAction = null;
            }
            else if (panelBasMonstre.gameObject.active)
            {
                buttonsMonster[1].selectObject();
                lastSelectedSort = null;
                lastSelectedAction = buttonsMonster[1];
            }
            else if (panelBasObject.gameObject.active)
            {
                buttonsObject[1].selectObject();
                lastSelectedSort = null;
                lastSelectedAction = buttonsObject[1];
            }
        }
        else if (key == KeyCode.Alpha3)
        {
            if (panelBasPrincipal.gameObject.active)
                showPanelBasSort();
            else if (panelBasSort.gameObject.active)
            {
                buttonsSort[2].selectSort();
                lastSelectedSort = buttonsSort[2];
                lastSelectedAction = null;
            }
            else if (panelBasMonstre.gameObject.active)
            {
                buttonsMonster[2].selectObject();
                lastSelectedSort = null;
                lastSelectedAction = buttonsMonster[2];
            }
            else if (panelBasObject.gameObject.active)
            {
                buttonsObject[2].selectObject();
                lastSelectedSort = null;
                lastSelectedAction = buttonsObject[2];
            }
        }
        else if (key == KeyCode.Alpha4)
        {
            if (panelBasSort.gameObject.active)
            {
                buttonsSort[3].selectSort();
                lastSelectedSort = buttonsSort[3];
                lastSelectedAction = null;
            }
            else if (panelBasMonstre.gameObject.active)
            {
                buttonsMonster[3].selectObject();
                lastSelectedSort = null;
                lastSelectedAction = buttonsMonster[3];
            }
            else if (panelBasObject.gameObject.active)
            {
                buttonsObject[3].selectObject();
                lastSelectedSort = null;
                lastSelectedAction = buttonsObject[3];
            }
        }
        else if (key == KeyCode.Alpha5)
        {
            if (panelBasSort.gameObject.active)
            {
                buttonsSort[4].selectSort();
                lastSelectedSort = buttonsSort[4];
                lastSelectedAction = null;
            }
            else if (panelBasMonstre.gameObject.active)
            {
                buttonsMonster[4].selectObject();
                lastSelectedSort = null;
                lastSelectedAction = buttonsMonster[4];
            }
            else if (panelBasObject.gameObject.active)
            {
                buttonsObject[4].selectObject();
                lastSelectedSort = null;
                lastSelectedAction = buttonsObject[4];
            }
        }
        else if (key == KeyCode.Tab)
        {
            if (lastSelectedAction != null)
                lastSelectedAction.selectObject();
            else if (lastSelectedSort != null)
                lastSelectedSort.selectSort();
        }
        else if (key == KeyCode.Escape)
        {
            if (panelBasMonstre.gameObject.active)
                hidePanelBasMonstre();
            else if (panelBasObject.gameObject.active)
                hidePanelBasObject();
            else if (panelBasSort.gameObject.active)
                hidePanelBasSort();
        }
    }

    /*Change le panel actif et visible*/
    public void hidePanelBasMonstre()
    {
        panelBasMonstre.gameObject.SetActive(false);
        panelBasPrincipal.gameObject.SetActive(true);
    }

    /*Change le panel actif et visible*/
    public void showPanelBasMonstre()
    {
        panelBasMonstre.gameObject.SetActive(true);
        panelBasPrincipal.gameObject.SetActive(false);
    }

    /*Change le panel actif et visible*/
    public void hidePanelBasObject()
    {
        panelBasObject.gameObject.SetActive(false);
        panelBasPrincipal.gameObject.SetActive(true);
    }

    /*Change le panel actif et visible*/
    public void showPanelBasObject()
    {
        panelBasObject.gameObject.SetActive(true);
        panelBasPrincipal.gameObject.SetActive(false);
    }

    /*Change le panel actif et visible*/
    public void hidePanelBasSort()
    {
        panelBasSort.gameObject.SetActive(false);
        panelBasPrincipal.gameObject.SetActive(true);
    }

    /*Change le panel actif et visible*/
    public void showPanelBasSort()
    {
        panelBasSort.gameObject.SetActive(true);
        panelBasPrincipal.gameObject.SetActive(false);
    }
}
