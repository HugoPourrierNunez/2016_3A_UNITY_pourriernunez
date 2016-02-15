using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LoadSceneScript : NetworkBehaviour {

    public void LoadMyGameScene()
    {
        Application.LoadLevel(1);
    }

    public void LoadMyMenuScene()
    {
        Application.LoadLevel(0);
    }
}
