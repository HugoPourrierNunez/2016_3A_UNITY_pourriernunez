﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

/*Classe qui gère le menu de connexion au réseau*/
public class NetworkMenuScript : MonoBehaviour {

    [SerializeField]
    NetworkManager manager;

    [SerializeField]
    ManageSoundScript soundScript;

    [SerializeField]
    Text ipText;

    public void StartHost()
    {
        if (ipText.text != "")
            manager.networkAddress = ipText.text;
        soundScript.OnStopSoundMenu();
        manager.StartHost();
    }

    public void StartClient()
    {
        if (ipText.text != "")
            manager.networkAddress = ipText.text;
        soundScript.OnStopSoundMenu();
        manager.StartClient();
    }
}
