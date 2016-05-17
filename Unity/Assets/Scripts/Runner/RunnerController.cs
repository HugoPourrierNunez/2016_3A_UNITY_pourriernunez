using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RunnerController : AbstractPlayerController
{
    [SerializeField]
    Camera runnerCamera;

    [SerializeField]
    Transform runner;

    [SerializeField]
    float jumpForce = 5f;

    [SerializeField]
    Rigidbody runnerRigidbody;

    [SerializeField]
    float vitesseMovement=.1f;

    [SerializeField]
    float vitesseGlobale = .1f;

    [SerializeField]
    Transform pointeur;

    [SerializeField]
    CapsuleCollider runnerVisualCollider;

    private GameObject pointedGO=null;

    // Use this for initialization
    public override void OnStartLocalPlayer () {
        base.OnStartLocalPlayer();
	    if(Camera.main && Camera.main.gameObject)
        {
            Camera.main.gameObject.SetActive(false);
        }
        runnerCamera.gameObject.SetActive(true);
	}

    void Update()
    {
        if(isLocalPlayer && controlActivated)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                CmdMove(Vector3.forward * vitesseMovement * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                CmdMove(Vector3.back * vitesseMovement * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                CmdMove(Vector3.left * vitesseMovement * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                CmdMove(Vector3.right * vitesseMovement * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(runnerRigidbody.velocity.y)<=0.0001f)
            {
                CmdJump(Vector3.up * jumpForce);
            }
            if(true) //Après il faudra faire en fonction de si le runner porte l'arme
            {
                Vector3 p1 = runnerCamera.transform.position;
                Vector3 p2 = runnerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, runnerCamera.farClipPlane));
                RaycastHit rayInfo;
                if(Physics.Linecast(p1, p2,out rayInfo))
                {
                    if(rayInfo.collider.gameObject!=runnerVisualCollider.gameObject)
                    {
                        pointeur.gameObject.SetActive(true);
                        pointeur.position = rayInfo.point;
                        pointedGO = rayInfo.collider.gameObject;
                    }
                    else
                    {
                        pointedGO = null;
                        pointeur.gameObject.SetActive(false);
                    }
                }
                else
                {
                    pointedGO = null;
                    pointeur.gameObject.SetActive(false);
                }
            }
            if(Input.GetMouseButtonDown(0))
            {
                if(pointedGO!=null && pointedGO.layer==LayerMask.NameToLayer("ObstacleDestroyable"))
                {
                    CmdUnactiveGameObject(pointedGO);
                    pointedGO = null;
                }
            }
            CmdAutoForward(Vector3.forward * Time.deltaTime * vitesseGlobale);
        }
    }

    [Command]
    public void CmdMove(Vector3 translateVector)
    {
        RpcMove(translateVector);
        if (!Network.isClient)
        {
            runner.transform.Translate(translateVector, Space.World);
        }
    }

    [ClientRpc]
    public void RpcMove(Vector3 translateVector)
    {
        runner.transform.Translate(translateVector, Space.World);
    }

    [Command]
    public void CmdJump(Vector3 translateVector)
    {
        RpcJump(translateVector);
        if (!Network.isClient)
        {
            runnerRigidbody.AddRelativeForce(translateVector);
        }
    }

    [ClientRpc]
    public void RpcJump(Vector3 translateVector)
    {
        runnerRigidbody.AddRelativeForce(translateVector);
    }

    [Command]
    public void CmdAutoForward(Vector3 velocityVector)
    {
        RpcAutoForward(velocityVector);
        if (!Network.isClient)
        {
            runner.Translate(velocityVector, Space.World);
        }
    }

    [ClientRpc]
    public void RpcAutoForward(Vector3 velocityVector)
    {
        runner.Translate(velocityVector, Space.World);
    }

    [Command]
    public void CmdUnactiveGameObject(GameObject go)
    {
        RpcUnactiveGameObject(go);
        if (!Network.isClient)
        {
            go.SetActive(false);
        }
    }

    [ClientRpc]
    public void RpcUnactiveGameObject(GameObject go)
    {
        go.SetActive(false);
    }
}