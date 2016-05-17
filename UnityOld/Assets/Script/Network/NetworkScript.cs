using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkScript : MonoBehaviour {

    [SerializeField]
    NetworkManager net;

    [SerializeField]
    UnityEngine.UI.Text ip;

    private ConnectionConfig conf=new ConnectionConfig();

	// Use this for initialization
	public void StartServerAndPlay () {
        net.StartHost(conf, 2);
	}

    public void JustPlay()
    {
        if (ip.text.Length > 0)
            net.networkAddress = ip.text;
        print(net.networkAddress);
        net.StartClient();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
