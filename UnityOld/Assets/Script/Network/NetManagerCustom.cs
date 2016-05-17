using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetManagerCustom : NetworkManager
{

  
   private bool host = false;

    // etc.

    GameObject chosenCharacter; // character1, character2, etc.

    // Instantiate whichever character the player chose and was assigned to chosenCharacter
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);

    }

    public override void OnStartHost()
    {
        print("host");
        host = true;
    }


}