using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class MasterController : AbstractPlayerController
{
    [SerializeField]
    Transform runnerView;

    [SerializeField]
    Transform floor;

    [SerializeField]
    Camera masterCamera;

    [SerializeField]
    Transform master;

    [SerializeField]
    float maxZoom=100;

    [SerializeField]
    float minZoom=-100;

    [SerializeField]
    float zoomSpeed = 1;

    [SerializeField]
    MasterUIManagerScript masterUI;

    [SerializeField]
    AllContainerScript allContainerScript;

    [SerializeField]
    int manaOnStart = 100;

    [SerializeField]
    int incomeMana = 1;

    [SerializeField]
    RunnerListScript runnerListScript;

    [SerializeField]
    PanelSortScript panelSortScript;

    [SerializeField]
    Light masterLigth;

    [SerializeField]
    Transform masterView;

    [SerializeField]
    ManageSoundScript manageSoundScript;

    private Vector3 positionCamera=new Vector3();
    private Vector3 translationCamera = new Vector3(0, 0, 0);
    private float effectiveZoom = 0;
    private float alignementGauche;

    private SpawnableObjectScript objectSelected = null;
    private AbstractSortScript sortSelected = null;
    private int mana;

    private RunnerController runnerController=null;
    private int runnerId = -1;
    private int numContainer=-1;


    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (Camera.main && Camera.main.gameObject)
        {
            Camera.main.gameObject.SetActive(false);
        }
        masterCamera.gameObject.SetActive(true);
        masterLigth.gameObject.SetActive(true);
        alignementGauche = getAlignGauche();
        mana = manaOnStart;
        InvokeRepeating("IncomeMana", 0, 1);

        int i = 0;
        runnerView = runnerListScript.getRunner(i).getView().transform;
        runnerId = i;
        runnerController = runnerListScript.getRunner(i);
        floor = runnerListScript.getRunner(i).getLevel().getFloor();
        masterView.localPosition = Vector3.right * floor.position.x + Vector3.up * masterView.localPosition.y + Vector3.forward * masterView.localPosition.z;
        masterUI.setRunnerFocused(i);

    }

    private void IncomeMana()
    {
        if(menuManager.getNumberOfPlayer()==3)
            mana += incomeMana*2;
        else mana += incomeMana;
        if (mana > manaOnStart) mana = manaOnStart;
        masterUI.getMasterManaBar().changePercentage(mana / (float)manaOnStart);
    }

    public void changePV(float percent, int ind)
    {
        //faire un test pour savoir si c'est le runner sur lequel on a le focus ou non
        //ou identifier si c'est le runner 1, 2 ou 3
        if(ind==0)
            masterUI.getRunnerPvBar().changePercentage(percent);
        else
            masterUI.getRunner2PvBar().changePercentage(percent);
    }

    private float getAlignGauche()
    {
        return (Mathf.Tan(masterCamera.fieldOfView) * Vector3.Distance(runnerView.transform.position, masterCamera.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                changeRunnerFocused();
            }
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                masterUI.keyPressed(KeyCode.Alpha1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                masterUI.keyPressed(KeyCode.Alpha2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                masterUI.keyPressed(KeyCode.Alpha3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                masterUI.keyPressed(KeyCode.Alpha4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                masterUI.keyPressed(KeyCode.Alpha5);
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                masterUI.keyPressed(KeyCode.Tab);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                masterUI.keyPressed(KeyCode.Escape);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && effectiveZoom > minZoom) // back
            {
                translationCamera.z = -zoomSpeed;
                effectiveZoom -= zoomSpeed; 
                alignementGauche = getAlignGauche();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0 && effectiveZoom < maxZoom) // forward
            {
                translationCamera.z = zoomSpeed;
                effectiveZoom += zoomSpeed;
                alignementGauche = getAlignGauche();
            }
            else
            {
                translationCamera.z = 0;
            }
            translationCamera.x = (runnerView.transform.position.z - masterCamera.transform.position.z) + alignementGauche;
            masterCamera.transform.Translate(translationCamera * zoomSpeed, Space.Self);

            if (objectSelected != null)
            {
                //print("Object selected");
                Vector3 p1 = masterCamera.transform.position;
                Vector3 p2 = masterCamera.ScreenToWorldPoint(Vector3.right*Input.mousePosition.x+ Vector3.up*Input.mousePosition.y+ Vector3.forward*masterCamera.farClipPlane);
                RaycastHit rayInfo;
                if (Physics.Linecast(p1, p2, out rayInfo))
                {
                    if (rayInfo.collider.gameObject == floor.gameObject)
                    {
                        p1.x = Mathf.Round(rayInfo.point.x);
                        p1.y = Mathf.Round(rayInfo.point.y)+.5f;
                        p1.z = Mathf.Round(rayInfo.point.z-.5f) + .5f;
                        objectSelected.UpdatePosition(p1, Vector3.Distance(p1, runnerView.position),runnerController.getLevel().isPositionOccuped(p1,false));
                    }
                    else objectSelected.Hide();
                }
                else objectSelected.Hide();
                if(objectSelected.CanBePosed() && Input.GetMouseButtonUp(0))
                {
                    removeMana(objectSelected.getCout());
                    //print("name =" + rayInfo.collider.gameObject.name);
                    CmdPoseObject(objectSelected.transform.position, runnerId);
                    runnerController.getLevel().setAllObstacleTransparent(false);
                }
                if (Input.GetMouseButtonUp(1))
                {
                    objectSelected.gameObject.SetActive(false);
                    objectSelected = null;
                    runnerController.getLevel().setAllObstacleTransparent(false);
                }
            }
            else if(sortSelected!=null)
            {
                Vector3 p1 = masterCamera.transform.position;
                Vector3 p2 = masterCamera.ScreenToWorldPoint(Vector3.right * Input.mousePosition.x + Vector3.up * Input.mousePosition.y + Vector3.forward * masterCamera.farClipPlane);
                RaycastHit rayInfo;
                if (Physics.Linecast(p1, p2, out rayInfo))
                {
                    if (rayInfo.collider.gameObject == floor.gameObject)
                    {
                        p1.x = Mathf.Round(rayInfo.point.x);
                        p1.y = Mathf.Round(rayInfo.point.y) + .5f;
                        p1.z = Mathf.Round(rayInfo.point.z - .5f) + .5f;
                        sortSelected.getSortVisualScript().gameObject.SetActive(true);
                        sortSelected.getSortVisualScript().setOK(false);
                        sortSelected.getSortVisualScript().updatePosition(p1);

                    }
                    else if(rayInfo.collider.gameObject.CompareTag("RunnerView"))
                    {
                        sortSelected.getSortVisualScript().gameObject.SetActive(true);
                        sortSelected.getSortVisualScript().setOK(true);
                        sortSelected.getSortVisualScript().updatePosition(rayInfo.collider.gameObject.transform.position);
                    }
                    else sortSelected.getSortVisualScript().gameObject.SetActive(false);
                }
                else
                {
                    sortSelected.getSortVisualScript().gameObject.SetActive(false);
                }

                if (sortSelected.getSortVisualScript().CanBeActivate() && Input.GetMouseButtonUp(0))
                {
                    int tmpRunner = runnerListScript.getRunnerIdByView(rayInfo.collider.gameObject);
                    if (tmpRunner != -1 && !sortSelected.affectAlreadyRunner(tmpRunner))
                    {
                        print("lauch sort");
                        removeMana((int)sortSelected.getCout());
                        CmdLauchSort(tmpRunner);
                    }
                    else
                        print("player already affected by this sort");
                    
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    sortSelected.getSortVisualScript().gameObject.SetActive(false);
                    sortSelected = null;
                }
            }
        }
    }

    public void changeRunnerFocused()
    {
        if(menuManager.getNumberOfPlayer()==3)
        {
            int i = runnerListScript.getRunnerIdByView(runnerView.gameObject);
            if (i == 0) i = 1;
            else i = 0;
            runnerView = runnerListScript.getRunner(i).getView().transform;
            runnerId = i;
            if(objectSelected!=null)
            {
                runnerController.getLevel().setAllObstacleTransparent(false);
                runnerController = runnerListScript.getRunner(i);
                runnerController.getLevel().setAllObstacleTransparent(true);
            }
            else
            {
                runnerController = runnerListScript.getRunner(i);
            }
            floor = runnerListScript.getRunner(i).getLevel().getFloor();
            masterView.localPosition = Vector3.right * floor.position.x + Vector3.up* masterView.localPosition.y+Vector3.forward* masterView.localPosition.z;
            masterUI.setRunnerFocused(i);


        }
    }

    public void removeMana(int nb)
    {
        mana -= nb;
        masterUI.getMasterManaBar().changePercentage(mana / (float)manaOnStart);
    }

    public void setSortSelected(int i)
    {
        CmdSetSortSelected(i);
    }

    public void setObjectSelected(int i, int j)
    {
        runnerController.getLevel().setAllObstacleTransparent(true);
        CmdSetObjectSelected(i, j);
    }

    public int getMana()
    {
        return mana;
    }

    public void DesactiveAll()
    {
        CmdDesactiveAll();
    }

    [Command]
    public void CmdDesactiveAll()
    {
        RpcDesactiveAll();
        if (!NetworkClient.active)
        {
            allContainerScript.DesactiveAll();
        }
    }

    [ClientRpc]
    public void RpcDesactiveAll()
    {
        allContainerScript.DesactiveAll();
    }

    [Command]
    public void CmdLauchSort(int tmpRunner)
    {
        RpcLauchSort(tmpRunner);
        if (!NetworkClient.active)
        {
            sortSelected.lauchSort(tmpRunner);
            sortSelected.getSortVisualScript().gameObject.SetActive(false);
            sortSelected = null;
        }
    }

    [ClientRpc]
    public void RpcLauchSort(int tmpRunner)
    {
        sortSelected.lauchSort(tmpRunner);
        sortSelected.getSortVisualScript().gameObject.SetActive(false);
        sortSelected = null;
    }

    [Command]
    public void CmdSetSortSelected(int i)
    {
        RpcSetSortSelected(i);
        if (!NetworkClient.active)
        {
            if (objectSelected != null)
            {
                objectSelected.gameObject.SetActive(false);
                objectSelected = null;
            }
            if (sortSelected != null)
            {
                sortSelected.getSortVisualScript().gameObject.SetActive(false);
            }

            sortSelected = panelSortScript.getSort(i);
        }
    }

    [ClientRpc]
    public void RpcSetSortSelected(int i)
    {
        if (objectSelected != null)
        {
            objectSelected.gameObject.SetActive(false);
            objectSelected = null;
        }
        if (sortSelected != null)
        {
            sortSelected.getSortVisualScript().gameObject.SetActive(false);
        }

        sortSelected = panelSortScript.getSort(i);
    }

    [Command]
    public void CmdHide()
    {
        RpcHide();
        if (!NetworkClient.active)
        {
            objectSelected.Hide();
        }
    }

    [ClientRpc]
    public void RpcHide()
    {
        objectSelected.Hide();
    }

    /*[Command]
    public void CmdUpdatePosition(Vector3 p1, float dist)
    {
        RpcUpdatePosition(p1, dist);
        if (!NetworkClient.active)
        {
            objectSelected.UpdatePosition(p1, dist);
        }
    }

    [ClientRpc]
    public void RpcUpdatePosition(Vector3 p1, float dist)
    {
        objectSelected.UpdatePosition(p1, dist);
    }*/

    [Command]
    public void CmdPoseObject(Vector3 pos,int runnerInd)
    {
        RpcPoseObject(pos, runnerInd);
        if (!NetworkClient.active)
        {
            objectSelected.PoseObject(pos, runnerInd);
            for (int i = 1; i < objectSelected.getNbSpawn(); i++)
            {
                SpawnableObjectScript obj = allContainerScript.getContainer(numContainer).getFirstDisableGO();
                if(obj!=null)
                {
                    obj.gameObject.SetActive(true);
                    obj.setLocalPlayerScript(localPlayerScript);
                    obj.UpdatePosition(pos, 0, false);
                    obj.PoseObject(pos, runnerInd);
                }
                
            }

            //objectSelected = null;
        }
    }

    [ClientRpc]
    public void RpcPoseObject(Vector3 pos, int runnerInd)
    {
        objectSelected.PoseObject(pos, runnerInd);
        for (int i = 1; i < objectSelected.getNbSpawn(); i++)
        {
            SpawnableObjectScript obj = allContainerScript.getContainer(numContainer).getFirstDisableGO();
            if (obj != null)
            {
                obj.gameObject.SetActive(true);
                obj.setLocalPlayerScript(localPlayerScript);
                obj.UpdatePosition(pos, 0, false);
                obj.PoseObject(pos, runnerInd);
            }
        }
        objectSelected = null;
    }

    [Command]
    public void CmdUnselectObject()
    {
        RpcUnselectObject();
        if (!NetworkClient.active)
        {
            objectSelected = null;
        }
    }

    [ClientRpc]
    public void RpcUnselectObject()
    {
        objectSelected = null;
    }

    [Command]
    public void CmdSetObjectSelected(int i, int j)
    {
        RpcSetObjectSelected(i, j);
        if (!NetworkClient.active)
        {
            numContainer = i;
            if (objectSelected != null)
                objectSelected.gameObject.SetActive(false);
            objectSelected = allContainerScript.getContainer(i).GetChildren()[j];
            objectSelected.gameObject.SetActive(true);
            objectSelected.setLocalPlayerScript(localPlayerScript);
            objectSelected.Hide();
        }
    }

    [ClientRpc]
    public void RpcSetObjectSelected(int i, int j)
    {
        numContainer = i;
        if (objectSelected != null)
            objectSelected.gameObject.SetActive(false);
        objectSelected = allContainerScript.getContainer(i).GetChildren()[j];
        objectSelected.gameObject.SetActive(true);
        objectSelected.setLocalPlayerScript(localPlayerScript);
        objectSelected.Hide();
    }

    public void GenerateLevel(float longueur, float largeur, float difficulty, float numberDestroyableObject, float numberUndestroyableObject)
    {
        for(int i=0;i<runnerListScript.getRunnerList().Count;i++)
        {
            //runnerListScript.getRunner(i).activeRB(false);
            runnerListScript.getRunner(i).getLevel().generateLevel(i, longueur, largeur, difficulty,numberDestroyableObject,numberUndestroyableObject);
            //runnerListScript.getRunner(i).activeRB(true);
        }
    }

    public void ActivePlayers()
    {
        CmdActivePlayers();
    }


    public override void RestartPlayer()
    {
        mana = manaOnStart;
    }

    public Transform getRunnerView()
    {
        return runnerView;
    }
    
    public void unactiveAllObstacles(int numRunner)
    {
        CmdUnactiveAllObstacles(numRunner);
    }

    public void activeDestroyableObstacle(Vector3 pos, int nb, int numRunner)
    {
        CmdActiveDestroyableObstacle(pos, nb, numRunner);
    }

    public void activeUndestroyableObstacle(Vector3 pos, int nb, int numRunner)
    {
        CmdActiveUndestroyableObstacle(pos, nb, numRunner);
    }

    public void changeSizeLevel(int numRunner, float longueur, float largueur)
    {
        CmdChangeSizeLevel(numRunner,longueur,largueur);
    }

    public void updateWaitingMenu()
    {
        if(isLocalPlayer)
            CmdUpdateWaitingMenu();
    }

    public void PlayMonster(int i, int j)
    {
        CmdPlayMonster(i, j);
    }

    public void DesactiveMonster(int i, int j)
    {
        if(NetworkClient.active)
            CmdDesactiveMonster(i, j);
    }

    [Command]
    public void CmdUpdateWaitingMenu()
    {
        RpcUpdateWaitingMenu();
        if (!NetworkClient.active)
        {
            menuManager.HideWaitingMenu();
        }

    }

    [ClientRpc]
    public void RpcUpdateWaitingMenu()
    {
        menuManager.HideWaitingMenu();
    }

    [Command]
    public void CmdActivePlayers()
    {
        RpcActivePlayers();
        if (!NetworkClient.active)
        {
            menuManager.HideWaitingMenuRunner();
            if (isLocalPlayer)
            {
                controlActivated = true;
                masterUI.gameObject.SetActive(true);
                for (int i = 0; i < runnerListScript.getRunnerList().Count; i++)
                {
                    print("active player : " + i);
                    runnerListScript.getRunner(i).RestartPlayer();
                }
            }
            else
            {
                for (int i = 0; i < runnerListScript.getRunnerList().Count; i++)
                {
                    RunnerController runner = runnerListScript.getRunner(i);
                    runner.RestartPlayer();
                    if (runner.isLocalPlayer)
                    {
                        runner.controlActivated = true;
                        runner.getUI().gameObject.SetActive(true);
                    }
                }
            }

            menuManager.setRunnerDead(0);

        }

    }

    [ClientRpc]
    public void RpcActivePlayers()
    {
        menuManager.HideWaitingMenuRunner();
        if (isLocalPlayer)
        {
            controlActivated = true;
            masterUI.gameObject.SetActive(true);
            for (int i = 0; i < runnerListScript.getRunnerList().Count; i++)
            {
                print("active player : " + i);
                runnerListScript.getRunner(i).RestartPlayer();
            }
        }
        else
        {
            for (int i = 0; i < runnerListScript.getRunnerList().Count; i++)
            {
                RunnerController runner = runnerListScript.getRunner(i);
                runner.RestartPlayer();
                if (runner.isLocalPlayer)
                {
                    runner.controlActivated = true;
                    runner.getUI().gameObject.SetActive(true);
                }
            }
        }
        menuManager.setRunnerDead(0);
    }

    [Command]
    public void CmdUnactiveAllObstacles(int numRunner)
    {
        RpcUnactiveAllObstacles(numRunner);
        if (!NetworkClient.active)
        {
            runnerListScript.getRunner(numRunner).getLevel().unactiveAllObstacles();
        }

    }

    [ClientRpc]
    public void RpcUnactiveAllObstacles(int numRunner)
    {
        runnerListScript.getRunner(numRunner).getLevel().unactiveAllObstacles();
    }

    [Command]
    public void CmdActiveDestroyableObstacle(Vector3 pos, int nb, int numRunner)
    {
        RpcActiveDestroyableObstacle(pos, nb, numRunner);
        if (!NetworkClient.active)
        {
            runnerListScript.getRunner(numRunner).getLevel().activeDestroyableObstacle(pos,nb);
        }

    }

    [ClientRpc]
    public void RpcActiveDestroyableObstacle(Vector3 pos, int nb, int numRunner)
    {
        runnerListScript.getRunner(numRunner).getLevel().activeDestroyableObstacle(pos, nb);
    }

    [Command]
    public void CmdActiveUndestroyableObstacle(Vector3 pos, int nb, int numRunner)
    {
        RpcActiveUndestroyableObstacle(pos, nb, numRunner);
        if (!NetworkClient.active)
        {
            runnerListScript.getRunner(numRunner).getLevel().activeUndestroyableObstacle(pos, nb);
        }

    }

    [ClientRpc]
    public void RpcActiveUndestroyableObstacle(Vector3 pos, int nb, int numRunner)
    {
        runnerListScript.getRunner(numRunner).getLevel().activeUndestroyableObstacle(pos, nb);
    }
    [Command]
    public void CmdChangeSizeLevel(int numRunner, float longueur, float largeur)
    {
        RpcChangeSizeLevel(numRunner, longueur, largeur);
        if (!NetworkClient.active)
        {
            runnerListScript.getRunner(numRunner).getLevel().changeSizeLevel(longueur,largeur);
        }

    }

    [ClientRpc]
    public void RpcChangeSizeLevel(int numRunner, float longueur, float largeur)
    {
        runnerListScript.getRunner(numRunner).getLevel().changeSizeLevel(longueur, largeur);
    }

    [Command]
    public void CmdPlayMonster(int i, int j)
    {
        RpcPlayMonster(i,j);
        if (!NetworkClient.active)
        {
            allContainerScript.getContainer(i).GetChildren()[j].Play();
        }
    }

    [ClientRpc]
    public void RpcPlayMonster(int i, int j)
    {
        allContainerScript.getContainer(i).GetChildren()[j].Play();
    }

    [Command]
    public void CmdDesactiveMonster(int i, int j)
    {
        RpcDesactiveMonster(i, j);
        if (!NetworkClient.active)
        {
            allContainerScript.getContainer(i).GetChildren()[j].Desactive();
        }
    }

    [ClientRpc]
    public void RpcDesactiveMonster(int i, int j)
    {
        allContainerScript.getContainer(i).GetChildren()[j].Desactive();
    }

}