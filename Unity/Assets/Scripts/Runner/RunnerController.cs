using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class RunnerController : AbstractPlayerController
{
    [SerializeField]
    LevelGeneratorScript level;

    [SerializeField]
    Camera runnerCamera;

    [SerializeField]
    RunnerCollisionScript runnerView;

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
    Material pointeurMaterial;

    [SerializeField]
    Color pointeurColor1 = Color.black;

    [SerializeField]
    Color pointeurColor2 = Color.red;

    [SerializeField]
    CapsuleCollider runnerVisualCollider;

    [SerializeField]
    LevelScript runnerLevel;

    [SerializeField]
    float maxPV=30;

    [SerializeField]
    RunnerUIManagerScript runnerUI;

    [SerializeField]
    MasterController masterController;

    [SerializeField]
    MenuManagerScript mmScript;

    [SerializeField]
    SortContainerScript sortContainerScript;

    private GameObject pointedGO=null;
    private Vector3 startPosition;
    private float PV;
    private const float timeElapse = .1f;


    List<AbstractSortScript> effectiveSorts = new List<AbstractSortScript>();

    IEnumerator Start()
    {
        while(true)
            yield return StartCoroutine(executeEffectiveSorts());
    }

    IEnumerator executeEffectiveSorts()
    {
        if (isLocalPlayer && controlActivated)
        {
            //print("nb effective sorts : " + effectiveSorts.Count);
            for (int i = 0; i < effectiveSorts.Count; i++)
            {
                if (!effectiveSorts[i].executeSort(this, timeElapse))
                {
                    CmdRemoveEffectiveSort(i);
                    i--;
                }
            }
            yield return new WaitForSeconds(timeElapse);
        }
    }

    public GameObject getView()
    {
        return runnerView.gameObject;
    }

    public void addEffectiveSort(int sortInd)
    {
        //CmdAddEffectiveSort(sortInd);
        effectiveSorts.Add(sortContainerScript.GetChildren()[sortInd]);
    }

    public bool hasAlreadySort(AbstractSortScript sort)
    {
        return effectiveSorts.Contains(sort);
    }

    public void Runnercollision(Collision col)
    {
        if (col.gameObject.layer != LayerMask.NameToLayer("Unfocusable") && col.gameObject!=level.getFloor().gameObject)
        {
            removePV(5f);
        }
    }

    public void removePV(float nb)
    {
        float percent;
        PV -= nb;
        if (PV <= 0)
        {
            PV = 0;
            percent = 0;
            mmScript.EndLevelShow();
            CmdEndLevel();
        }
        else
        {
            percent = PV / maxPV;
        }

        runnerUI.getPvBar().changePercentage(percent);
        CmdDisplayMasterPV(percent);
    }

    // Use this for initialization
    public override void OnStartLocalPlayer () {
        base.OnStartLocalPlayer();
	    if(Camera.main && Camera.main.gameObject)
        {
            Camera.main.gameObject.SetActive(false);
        }
        runnerCamera.gameObject.SetActive(true);
        startPosition = runnerView.transform.position;
        runnerView.setRunnerController(this);
        RestartPlayer();
        runnerLevel.activate();
    }

    public override void RestartPlayer()
    {
        PV = maxPV;
        runnerView.transform.position = startPosition;
        level.generateLevel();
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
                    if(rayInfo.collider.gameObject!=runnerVisualCollider.gameObject && rayInfo.collider.gameObject.layer != LayerMask.NameToLayer("Unfocusable"))
                    {
                        pointeur.gameObject.SetActive(true);
                        
                        if (rayInfo.collider.gameObject.layer != LayerMask.NameToLayer("ObstacleDestroyable"))
                        {
                            pointeurMaterial.color = pointeurColor1;
                            pointeurMaterial.SetColor("_EmissionColor", pointeurColor1);
                        }
                        else
                        {
                            pointeurMaterial.color = pointeurColor2;
                            pointeurMaterial.SetColor("_EmissionColor", pointeurColor2);
                        }
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
            CmdMove(Vector3.forward * vitesseGlobale * Time.deltaTime);
            UpdateAvancement();
        }
    }

    private void UpdateAvancement()
    {
        runnerUI.getavancementBar().changePercentage(runnerView.transform.position.z/(runnerLevel.getFloor().localScale.z*10));
    }

    [Command]
    public void CmdRemoveEffectiveSort(int i)
    {
        RpcRemoveEffectiveSort(i);
        if (!NetworkClient.active)
        {
            effectiveSorts[i].removePlayer(this);
            effectiveSorts.RemoveAt(i);
        }
    }

    [ClientRpc]
    public void RpcRemoveEffectiveSort(int i)
    {
        //print("//////////////////////////////////////////remove i=" + i);
        effectiveSorts[i].removePlayer(this);
        effectiveSorts.RemoveAt(i) ;
    }

    [Command]
    public void CmdEndLevel()
    {
        RpcEndLevel();
        if (!NetworkClient.active)
        {
            for(int i =0; i<effectiveSorts.Count;i++)
            {
                effectiveSorts[i].removePlayer(this);
            }
            effectiveSorts.Clear();
        }
    }

    [ClientRpc]
    public void RpcEndLevel()
    {
        for (int i = 0; i < effectiveSorts.Count; i++)
        {
            effectiveSorts[i].removePlayer(this);
        }
        effectiveSorts.Clear();
    }

    /*[Command]
    public void CmdAddEffectiveSort(int sortInd)
    {
        RpcAddEffectiveSort(sortInd);
        if (!NetworkClient.active)
        {
            effectiveSorts.Add(sortContainerScript.GetChildren()[sortInd]);
        }
    }

    [ClientRpc]
    public void RpcAddEffectiveSort(int sortInd)
    {
        print("add sort");
        effectiveSorts.Add(sortContainerScript.GetChildren()[sortInd]);
    }*/

    [Command]
    public void CmdMove(Vector3 translateVector)
    {
        RpcMove(translateVector);
        if (!NetworkClient.active)
        {
            runnerView.transform.Translate(translateVector, Space.World);
        }
    }

    [ClientRpc]
    public void RpcMove(Vector3 translateVector)
    {
        runnerView.transform.Translate(translateVector, Space.World);
    }

    [Command]
    public void CmdJump(Vector3 translateVector)
    {
        RpcJump(translateVector);
        if (!NetworkClient.active)
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
        if (!NetworkClient.active)
        {
            runnerView.transform.Translate(velocityVector, Space.World);
        }
    }

    [ClientRpc]
    public void RpcAutoForward(Vector3 velocityVector)
    {
        runnerView.transform.Translate(velocityVector, Space.World);
    }

    [Command]
    public void CmdUnactiveGameObject(GameObject go)
    {
        RpcUnactiveGameObject(go);
        if (!NetworkClient.active)
        {
            go.SetActive(false);
        }
    }

    [ClientRpc]
    public void RpcUnactiveGameObject(GameObject go)
    {
        go.SetActive(false);
    }

    [Command]
    public void CmdDisplayMasterPV(float percent)
    {
        RpcDisplayMasterPV(percent);
        if (!NetworkClient.active)
        {
            masterController.changePV(percent);
        }
        
    }

    [ClientRpc]
    public void RpcDisplayMasterPV(float percent)
    {
        masterController.changePV(percent);
    }
}