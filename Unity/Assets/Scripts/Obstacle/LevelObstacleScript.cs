using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LevelObstacleScript : NetworkBehaviour {

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("RunnerView"))
        {
            gameObject.SetActive(false);
        }
    }
}
