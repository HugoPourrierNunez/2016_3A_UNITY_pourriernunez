using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MasterController : AbstractPlayerController
{
    [SerializeField]
    Transform runnerView;

    [SerializeField]
    Camera masterCamera;

    [SerializeField]
    Transform master;

    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (Camera.main && Camera.main.gameObject)
        {
            Camera.main.gameObject.SetActive(false);
        }
        masterCamera.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        masterCamera.transform.position= new Vector3(masterCamera.transform.position.x, masterCamera.transform.position.y, runnerView.position.z);
        /*if (Input.GetKeyDown(KeyCode.Z) && isLocalPlayer)
        {
            CmdForward();
        }
        else if (Input.GetKeyDown(KeyCode.S) && isLocalPlayer)
        {
            CmdBackward();
        }*/
    }

    [Command]
    public void CmdForward()
    {
        RpcForward();
        if (!Network.isClient)
        {
            master.transform.Translate(Vector3.forward);
        }
    }

    [ClientRpc]
    public void RpcForward()
    {
        master.transform.Translate(Vector3.forward);
    }

    [Command]
    public void CmdBackward()
    {
        RpcBackward();
        if (!Network.isClient)
        {
            master.transform.Translate(-Vector3.forward);
        }
    }

    [ClientRpc]
    public void RpcBackward()
    {
        master.transform.Translate(-Vector3.forward);
    }
}