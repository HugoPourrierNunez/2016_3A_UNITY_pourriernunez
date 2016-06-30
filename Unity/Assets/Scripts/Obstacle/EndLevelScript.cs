using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EndLevelScript : NetworkBehaviour
{

    [SerializeField]
    MenuManagerScript mmScript;

    void OnTriggerEnter(Collider other)
    {
        mmScript.EndLevelShow(true);
    }
}
