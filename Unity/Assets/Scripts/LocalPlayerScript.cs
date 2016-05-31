using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LocalPlayerScript : NetworkBehaviour {

    public AbstractPlayerController localPlayer { get; set; }

    public NetworkConnection connection { get; set; }
}
