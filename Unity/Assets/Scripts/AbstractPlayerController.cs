using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class AbstractPlayerController : NetworkBehaviour
{

    [SerializeField]
    protected LocalPlayerScript localPlayerScript;

    [SerializeField]
    protected MenuManagerScript menuManager;


    public bool controlActivated { get; set; }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        localPlayerScript.localPlayer = this;
        controlActivated = false;
        CmdPreventMenuManagerOfNewPlayer();
    }

    [Command]
    public void CmdPreventMenuManagerOfNewPlayer()
    {
        int nbActivePlayer = menuManager.getNumberOfActivePlayers()+1;
        int nbPlayer = menuManager.getNumberOfPlayer();
        RpcPreventMenuManagerOfNewPlayer(nbActivePlayer, nbPlayer);
        if (!NetworkClient.active)
        {
            menuManager.setNumberOfPlayer(nbPlayer);
            menuManager.setNumberOfActivePlayers(nbActivePlayer);
        }
    }

    [ClientRpc]
    public void RpcPreventMenuManagerOfNewPlayer(int nbActivePlayer, int nbPlayer)
    {
        menuManager.setNumberOfPlayer(nbPlayer);
        menuManager.setNumberOfActivePlayers(nbActivePlayer);
    }

    public abstract void RestartPlayer();

    public void setNetworkConnection(NetworkConnection conn,CustomNetworkManager networkManager)
    {
        localPlayerScript.connection = conn;
        localPlayerScript.manager = networkManager;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
