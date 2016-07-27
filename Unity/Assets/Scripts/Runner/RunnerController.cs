using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

/*Classe qui gère toutes les actions des runners*/
public class RunnerController : AbstractPlayerController
{
    [SerializeField]
    AllContainerScript allContainerScript;

    [SerializeField]
    ManageSoundScript soundScript;

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

    [SerializeField]
    Light runnerLight;

    [SerializeField]
    Vector3 startPosition;

    [SerializeField]
    ConstantForce runnerConstantForce;

    [SerializeField]
    RunnerListScript runnerList;

    private GameObject pointedGO=null;
    private float PV;
    private const float timeElapse = .1f;
    private WaitForSeconds wait = new WaitForSeconds(timeElapse);
    private bool right=false,left=false,forward=false,back=false;


    List<AbstractSortScript> effectiveSorts = new List<AbstractSortScript>();

    IEnumerator Start()
    {
        while(true)
            yield return StartCoroutine(executeEffectiveSorts());
    }

    /*Fonction utilisé par une coroutine qui appéle les actions des sort actifs sur le joueur à un interval donné*/
    IEnumerator executeEffectiveSorts()
    {
        if (isLocalPlayer && controlActivated)
        {
            for (int i = 0; i < effectiveSorts.Count; i++)
            {
                if (!effectiveSorts[i].executeSort(this, timeElapse))
                {
                    CmdRemoveEffectiveSort(i);
                    i--;
                }
            }
            yield return wait;
        }
    }

    public void DesactiveObstacle(int i,bool isDestroyable)
    {
        if(isLocalPlayer)
            CmdDesactiveObstacleLevel(i, isDestroyable);
    }

    /*Retourne l'interface utilisateur du joueur*/
    public RunnerUIManagerScript getUI()
    {
        return runnerUI;
    }

    /*Retourne le visuel du runner*/
    public GameObject getView()
    {
        return runnerView.gameObject;
    }

    /*Désactive un object dans un conteneur i au rang j*/
    public void DesactiveObject(int i,int j)
    {
        CmdDesactiveObject(i, j);
    }

    /*Ajoute un sort actif sur le runner*/
    public void addEffectiveSort(int sortInd)
    {
        effectiveSorts.Add(sortContainerScript.GetChildren()[sortInd]);
    }

    /*Retourne un boolean pour savoir si le runner a déjà ce sort qui lui est appliqué*/
    public bool hasAlreadySort(AbstractSortScript sort)
    {
        return effectiveSorts.Contains(sort);
    }

    /*Méthode appelé lorsque le joueur entre en collision avec quelque chose*/
    public void Runnercollision(Collision col)
    {
        if (col.gameObject.layer != LayerMask.NameToLayer("Unfocusable") && col.gameObject!=level.getFloor().gameObject)
        {
            soundScript.OnPlaySoundCollision();
            removePV(5f);
        }
    }

    /*Enlève les pv au joueur et synchro l'action sur le réseau*/
    public void RemovePVNetwork(float nb)
    {
        CmdRemovePV(nb);
    }

    /*Enlève des pv au runner*/
    public void removePV(float nb)
    {
        float percent;
        PV -= nb;
        if (PV <= 0)
        {
            PV = 0;
            percent = 0;
            soundScript.OnPlaySoundGameOver();
            if(controlActivated)
                CmdEndLevel();
        }
        else
        {
            percent = PV / maxPV;
        }
        runnerUI.getPvBar().changePercentage(percent);
        if(!NetworkServer.active)
            CmdDisplayMasterPV(percent, runnerList.getRunnerList().IndexOf(this));
    }

    public void addPV(float nb)
    {
        float percent;
        PV += nb;
        if (PV >= maxPV)
        {
            PV = maxPV;
            percent = 1;
        }
        else
        {
            percent = PV / maxPV;
        }
        runnerUI.getPvBar().changePercentage(percent);
        if (!NetworkServer.active)
            CmdDisplayMasterPV(percent, runnerList.getRunnerList().IndexOf(this));
    }

    // Use this for initialization
    public override void OnStartLocalPlayer () {
        base.OnStartLocalPlayer();
	    if(Camera.main && Camera.main.gameObject)
        {
            Camera.main.gameObject.SetActive(false);
        }

        runnerLight.gameObject.SetActive(true);
        runnerCamera.gameObject.SetActive(true);
        runnerView.setRunnerController(this);
        RestartPlayer();
        runnerLevel.activate();
    }

    /*Retourne la lumière du runner*/
    public Light getLight()
    {
        return runnerLight;
    }

    /*remet à zéro les données du runner*/
    public override void RestartPlayer()
    {
        runnerRigidbody.isKinematic = false;
        runnerView.transform.localPosition = startPosition;
        PV = maxPV;
        mmScript.getEndMenuRunner().gameObject.SetActive(false);
        runnerUI.getPvBar().changePercentage(1);
        if(isLocalPlayer)
            CmdDisplayMasterPV(1, runnerList.getRunnerList().IndexOf(this));
    }

    void FixedUpdate()
    {
        if (isLocalPlayer && controlActivated)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                CmdMove(Vector3.forward * vitesseMovement + Vector3.forward * vitesseGlobale);
                forward = true;
            }
            else
                forward = false;
            if (Input.GetKey(KeyCode.S))
            {
                CmdMove(Vector3.back * vitesseMovement + Vector3.forward * vitesseGlobale);
                back = true;
            }
            else
                back = false;
            if (Input.GetKey(KeyCode.Q))
            {
                if (runnerRigidbody.velocity.z < vitesseGlobale / 10)
                    CmdMove(Vector3.left * vitesseMovement * 2);
                else
                    CmdMove(Vector3.left * vitesseMovement * 2 + Vector3.forward * vitesseGlobale);
                left = true;
            }
            else
                left = false;
            if (Input.GetKey(KeyCode.D))
            {
                if (runnerRigidbody.velocity.z < vitesseGlobale / 10)
                    CmdMove(Vector3.right * vitesseMovement * 1.5f);
                else
                    CmdMove(Vector3.right * vitesseMovement * 1.5f + Vector3.forward * vitesseGlobale);
                right = true;
            }
            else
                right = false;
            if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Z))
            {
                CmdMove(Vector3.forward * vitesseGlobale);
            }
        }
        else if(isLocalPlayer && !controlActivated && mmScript.getEndMenuRunner().gameObject.active && !runnerRigidbody.isKinematic)
        {
            CmdKinematic(true);
        }
    }

    void Update()
    {
        if(isLocalPlayer && controlActivated)
        {
            if(true) 
            {
                Vector3 p1 = runnerCamera.transform.position;
                Vector3 p2 = runnerCamera.ScreenToWorldPoint(Vector3.right*Input.mousePosition.x+Vector3.up* Input.mousePosition.y+Vector3.forward* runnerCamera.farClipPlane);
                RaycastHit rayInfo;
                if(Physics.Linecast(p1, p2,out rayInfo))
                {
                    if(rayInfo.collider.gameObject!=runnerVisualCollider.gameObject && rayInfo.collider.gameObject.layer != LayerMask.NameToLayer("Unfocusable"))
                    {
                        pointeur.gameObject.SetActive(true);
                        
                        if (rayInfo.collider.gameObject.layer != LayerMask.NameToLayer("ObstacleDestroyable") && !rayInfo.collider.gameObject.CompareTag("Destroyable"))
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
                if(pointedGO!=null && (pointedGO.layer==LayerMask.NameToLayer("ObstacleDestroyable") || pointedGO.CompareTag("Destroyable")))
                {
                    int ind = level.getIndexDestroyableObstacle(pointedGO);
                    if (ind != -1)
                        CmdUnactiveGameObject(ind);
                    else
                    {
                        pointedGO.SetActive(false);
                    }
                        
                    pointedGO = null;
                }
            }
            UpdateAvancement();
        }
    }

    /*Met à jour l'avancement du runner */
    private void UpdateAvancement()
    {
        runnerUI.getavancementBar().changePercentage(runnerView.transform.position.z/(runnerLevel.getFloor().localScale.z*10));
    }

    /*Active ou non le rigidbody du runner*/
    public void activeRB(bool activate)
    {
        runnerRigidbody.isKinematic = activate;
    }

    /*Retourne le level du runner*/
    public LevelGeneratorScript getLevel()
    {
        return level;
    }

    [Command]
    public void CmdRemovePV(float nb)
    {
        RpcRemovePV(nb);
        if (!NetworkClient.active)
        {
            removePV(nb);
        }
    }

    [ClientRpc]
    public void RpcRemovePV(float nb)
    {
        removePV(nb);
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
        effectiveSorts[i].removePlayer(this);
        effectiveSorts.RemoveAt(i) ;
    }

    [Command]
    public void CmdEndLevel()
    {
        soundScript.OnStopSoundInGame();
        RpcEndLevel();
        if (!NetworkClient.active)
        {
            menuManager.setRunnerDead(menuManager.getRunnerDead() + 1);
            if (isLocalPlayer)
            {
                if (PV > 0)
                    mmScript.EndLevelShow(true);
                else mmScript.EndLevelShow(false);
            }
            if (menuManager.getRunnerDead() == menuManager.getNumberOfPlayer() - 1)
            {

                if (isLocalPlayer && PV > 0)
                    mmScript.EndLevelShow(true);
                else mmScript.EndLevelShow(false);
                for (int i = 0; i < effectiveSorts.Count; i++)
                {
                    effectiveSorts[i].removePlayer(this);
                }
                effectiveSorts.Clear();
                menuManager.setRunnerDead(0); ;
            }

        }
    }

    [ClientRpc]
    public void RpcEndLevel()
    {
        menuManager.setRunnerDead(menuManager.getRunnerDead() + 1);
        if (isLocalPlayer)
        {
            if (PV > 0)
                mmScript.EndLevelShow(true);
            else mmScript.EndLevelShow(false);
        }
        if (menuManager.getRunnerDead() == menuManager.getNumberOfPlayer() - 1)
        {
                
            if (isLocalPlayer && PV > 0)
                mmScript.EndLevelShow(true);
            else mmScript.EndLevelShow(false);
            for (int i = 0; i < effectiveSorts.Count; i++)
            {
                effectiveSorts[i].removePlayer(this);
            }
            effectiveSorts.Clear();
            menuManager.setRunnerDead(0); ;
        }
        
        
    }

    [Command]
    public void CmdMove(Vector3 translateVector)
    {
        RpcMove(translateVector);
        if (!NetworkClient.active)
        {
            runnerRigidbody.velocity =  translateVector;
        }
    }

    [ClientRpc]
    public void RpcMove(Vector3 translateVector)
    {
        runnerRigidbody.velocity = translateVector;
    }

    [Command]
    public void CmdKinematic(bool isKine)
    {
        RpcKinematic(isKine);
        if (!NetworkClient.active)
        {
            runnerRigidbody.isKinematic = isKine;
        }
    }

    [ClientRpc]
    public void RpcKinematic(bool isKine)
    {
        runnerRigidbody.isKinematic = isKine;
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
    public void CmdUnactiveGameObject(int i) 
    {
        RpcUnactiveGameObject(i);
        if (!NetworkClient.active)
        {
            addPV(1);
            level.DesactiveDestroyableObstacle(i);
        }
    }

    [ClientRpc]
    public void RpcUnactiveGameObject(int i)
    {
        addPV(1);
        level.DesactiveDestroyableObstacle(i);
    }

    [Command]
    public void CmdDisplayMasterPV(float percent, int ind)
    {
        RpcDisplayMasterPV(percent,ind);
        if (!NetworkClient.active)
        {
            masterController.changePV(percent,ind);
        }
        
    }

    [ClientRpc]
    public void RpcDisplayMasterPV(float percent, int ind)
    {
        masterController.changePV(percent,ind);
    }

    [Command]
    public void CmdDesactiveObject(int i, int j)
    {
        RpcDesactiveObject(i, j);
        if (!NetworkClient.active)
        {
            SpawnableObjectScript obj = allContainerScript.getContainer(i).GetChildren()[j];
            obj.Desactive();
            level.setPositionNotOccuped(obj.gameObject.transform.position, false);
        }
    }

    [ClientRpc]
    public void RpcDesactiveObject(int i, int j)
    {
        allContainerScript.getContainer(i).GetChildren()[j].Desactive();
    }

    [Command]
    public void CmdDesactiveObstacleLevel(int i, bool destroyable)
    {
        RpcDesactiveObstacleLevel(i,destroyable);
        if (!NetworkClient.active)
        {
            if(destroyable)
                level.DesactiveDestroyableObstacle(i);
            else
                level.DesactiveUndestroyableObstacle(i);
        }
    }

    [ClientRpc]
    public void RpcDesactiveObstacleLevel(int i, bool destroyable)
    {
        if (destroyable)
            level.DesactiveDestroyableObstacle(i);
        else
            level.DesactiveUndestroyableObstacle(i);
    }
}