using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/*Classe qui permet de savoir quand la fin de niveau est touché par le joueur*/
public class EndLevelScript : NetworkBehaviour
{

    [SerializeField]
    MenuManagerScript mmScript;

    void OnTriggerEnter(Collider other)
    {
        mmScript.EndLevelShow(true);
    }
}
