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
    HBarScript runner2PvBar;

    [SerializeField]
    GameObject runner2PVPanel;

    [SerializeField]
    Image backgroundRunner1;

    [SerializeField]
    Image backgroundRunner2;

    [SerializeField]
    HBarScript masterManaBar;

    private Color colorFocused = new Color(1, 1, 1, 1);
    private Color colorUnfocused = new Color(1, 1, 1, .39f);

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
        }
        else if (key == KeyCode.Alpha2)
        {
            if (panelBasPrincipal.gameObject.active)
                showPanelBasObject();
        }
        else if (key == KeyCode.Alpha3)
        {
            if (panelBasPrincipal.gameObject.active)
                showPanelBasSort();
        }
        else if (key == KeyCode.Alpha4)
        {
            
        }
        else if (key == KeyCode.Alpha5)
        {
            
        }
        else if (key == KeyCode.Tab)
        {
            
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
