using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class AbstractPlayerController : NetworkBehaviour
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

    public abstract void RestartPlayer();

    public void setNetworkConnection(NetworkConnection conn)
    {
        localPlayerScript.connection = conn;
    }
}