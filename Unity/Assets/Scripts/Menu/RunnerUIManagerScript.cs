using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*Classe utilisé pour gérer l'UI du Runner*/
public class RunnerUIManagerScript : MonoBehaviour {

    [SerializeField]
    HBarScript pvBar;

    [SerializeField]
    VBarScript avancementBar;

    /*Retourne la bar d'avancement des pv du runner*/
    public HBarScript getPvBar()
    {
        return pvBar;
    }

    /*Retourne la barre d'avancement de la progression du joueur sur le niveau*/
    public VBarScript getavancementBar()
    {
        return avancementBar;
    }
}
