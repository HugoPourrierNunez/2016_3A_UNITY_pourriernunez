using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class SpawningScript : NetworkBehaviour {

    private int maxPlayer = 4;
    int playerCount;
    int idMaster;

    [SerializeField]
    Transform spawnMaster;

    [SerializeField]
    Transform spawnRunner;

    [SerializeField]
    Camera cameraPlayer; // C'est pour tester, donc je mets seulement la caméra

	// Use this for initialization
	void Start () {

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
	
    void OnServerAddPlayer()
    {
        int idPlayer = playerCount;

        if (idPlayer == idMaster)
        {
            // Si l'idPlayer correspond à l'idMaster alors on fait spawn le joueur au spawn Master
            cameraPlayer.transform.Rotate(spawnMaster.transform.position);
            
            // Rester à réussir à donner la rotation de la caméra
        }
        else
        {
            // Autrement, il spawn en tant que Runner
            cameraPlayer.transform.Rotate(spawnRunner.transform.position);

            // Même chose pour la rotation
        }

        playerCount++;
        PlayerPrefs.SetInt("CountPlayer", playerCount);
    }
}
