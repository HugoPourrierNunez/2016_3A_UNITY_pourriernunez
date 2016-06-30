using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WalkingMonsterScript : SpawnableObjectScript {

    [SerializeField]
    float vitesse;

    [SerializeField]
    Rigidbody rb;

    float interval = .1f;
    float nextTime = 0;

    // Use this for initialization
    void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= nextTime)
        {
            if(NetworkServer.active)
            {
                //do something here every interval seconds
                print("move");
                CmdMove();
            }
            nextTime += interval;

        }
    }

    [Command]
    public void CmdMove()
    {
        RpcMove();
        if (!NetworkClient.active)
        {
            rb.AddForce(Vector3.up*vitesse);
        }
    }

    [ClientRpc]
    public void RpcMove()
    {
        rb.AddForce(Vector3.up * vitesse);
    }
    
}
