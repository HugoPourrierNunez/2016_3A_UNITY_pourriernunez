using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkMenuScript : MonoBehaviour {

    [SerializeField]
    NetworkManager manager;

    public void StartHost()
    {
        manager.StartHost();
    }

    public void StartClient()
    {
        manager.StartClient();
    }

    public void Exit()
    {

    }
}
