using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AbstractPlayerController : NetworkBehaviour
{

    [SerializeField]
    LocalPlayerScript localPlayerScript;

    public bool controlActivated { get; set; }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        localPlayerScript.localPlayer=this;
        controlActivated = false;
    }
}