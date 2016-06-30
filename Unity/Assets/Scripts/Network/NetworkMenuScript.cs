using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkMenuScript : MonoBehaviour {

    [SerializeField]
    NetworkManager manager;

    [SerializeField]
    Text ipText;

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
