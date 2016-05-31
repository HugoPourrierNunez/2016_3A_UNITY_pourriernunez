using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CustomNetworkManager : NetworkManager
{
    Dictionary<NetworkConnection, AbstractPlayerController> activatedGamerInstance = null;

    public Dictionary<NetworkConnection, AbstractPlayerController> ActivatedControllers
    {
        get
        {
            if (null == activatedGamerInstance)
            {
                activatedGamerInstance = new Dictionary<NetworkConnection, AbstractPlayerController>();
            }

            return activatedGamerInstance;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        // Do not forget base functionalities
        base.OnServerAddPlayer(conn, playerControllerId);

        // Let's get the next player Controller
        var avatar = GamerInstanceManager.Instance.GetNextAvailableController();

        // Let's save the controller/connection association
        ActivatedControllers.Add(conn, avatar);

        // give authority to the client on the obtained controller
        NetworkServer.ReplacePlayerForConnection(conn, avatar.gameObject, playerControllerId);

        avatar.setNetworkConnection(conn);

    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // Clear everything if the association is known
        if (activatedGamerInstance.ContainsKey(conn))
        {
            conn.playerControllers.Clear();
            GamerInstanceManager.Instance.ReleaseController(activatedGamerInstance[conn]);
            ActivatedControllers.Remove(conn);
        }

        // Do not forget base functionalities
        base.OnServerDisconnect(conn);
    }

}
