using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    HBarScript masterManaBar;

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
