using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;


public class LoadSceneScript : NetworkBehaviour {

    public void LoadMyGameScene()
    {
        SceneManager.LoadScene("InGameRunnerOnline");
    }

    public void LoadMyMenuScene()
    {
        SceneManager.LoadScene("InMenu");
    }
}
