using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/*Classe qui référence le localplayer et qui sert à tout le monde à y accéder*/
public class LocalPlayerScript : NetworkBehaviour {

    public AbstractPlayerController localPlayer { get; set; }

    public NetworkConnection connection { get; set; }

    public CustomNetworkManager manager { get; set; }

}
