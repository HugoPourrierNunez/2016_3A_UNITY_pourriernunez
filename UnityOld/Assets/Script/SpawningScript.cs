using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class SpawningScript : NetworkBehaviour {

    private int maxPlayer = 4;
    int playerCount;
    int idMaster;

    [SerializeField]
    Camera camRunner;

    [SerializeField]
    Camera camMaster;

    [SerializeField]
    GameObject goMaster;

    [SerializeField]
    GameObject goRunner;

    [SerializeField]
    Renderer renderer;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            renderer.enabled = false;
            playerCount = PlayerPrefs.GetInt("CountPlayer", playerCount); // Permet de sauvegarder des variables
            idMaster = PlayerPrefs.GetInt("IdMaster", idMaster);

            if (idMaster == null)
            {
                idMaster = Random.Range(1, 4);
                Debug.Log("IdMaster = " + idMaster);
                PlayerPrefs.SetInt("IdMaster", idMaster);
            }

            if (playerCount == maxPlayer)
            {
                Debug.Log("Nombre de joueur maximum, atteint");
            }
        }
	}
	
    void OnServerAddPlayer()
    {
        if (isLocalPlayer)
        {
            int idPlayer = playerCount;

            if (idPlayer == idMaster)
            {
                goMaster.SetActive(true);
                camMaster.enabled = true;

                goRunner.SetActive(false);
                camRunner.enabled = false;
                renderer.enabled = false;
                Debug.Log("Je suis le Master");
            }
            else
            {
                goMaster.SetActive(false);
                camMaster.enabled = false;

                goRunner.SetActive(true);
                camRunner.enabled = true;
                renderer.enabled = true;
                Debug.Log("Je suis un Runner");
            }

            playerCount++;
            PlayerPrefs.SetInt("CountPlayer", playerCount);
        }
    }
}