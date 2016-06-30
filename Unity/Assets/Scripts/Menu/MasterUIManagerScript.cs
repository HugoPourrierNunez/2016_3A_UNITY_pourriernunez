using UnityEngine;
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

    [SerializeField]
    HBarScript runnerPvBar;

    [SerializeField]
    HBarScript masterManaBar;

    public HBarScript getMasterManaBar()
    {
        return masterManaBar;
    }

    public HBarScript getRunnerPvBar()
    {
        return runnerPvBar;
    }


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
}
