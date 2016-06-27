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
    LocalPlayerScript localPlayerScript;

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

    private Vector3 positionCamera=new Vector3();
    private Vector3 translationCamera = new Vector3(0, 0, 0);
    private float effectiveZoom = 0;
    private float alignementGauche;

    private SpawnableObjectScript objectSelected = null;
    private AbstractSortScript sortSelected = null;
    private int mana;


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
    }

    private void IncomeMana()
    {
        mana += incomeMana;
        if (mana > manaOnStart) mana = manaOnStart;
        masterUI.getMasterManaBar().changePercentage(mana / (float)manaOnStart);
    }

    public void changePV(float percent)
    {
        //faire un test pour savoir si c'est le runner sur lequel on a le focus ou non
        //ou identifier si c'est le runner 1, 2 ou 3
        masterUI.getRunnerPvBar().changePercentage(percent);
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
                Vector3 p2 = masterCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, masterCamera.farClipPlane));
                RaycastHit rayInfo;
                if (Physics.Linecast(p1, p2, out rayInfo))
                {
                    if (rayInfo.collider.gameObject == floor.gameObject)
                    {
                        p1.x = Mathf.Round(rayInfo.point.x);
                        p1.y = Mathf.Round(rayInfo.point.y)+.5f;
                        p1.z = Mathf.Round(rayInfo.point.z-.5f) + .5f;
                        objectSelected.UpdatePosition(p1, Vector3.Distance(p1, runnerView.position));
                    }
                    else objectSelected.Hide();
                }
                else objectSelected.Hide();
                if(objectSelected.CanBePosed() && Input.GetMouseButtonUp(0))
                {
                    removeMana(objectSelected.getCout());
                    CmdPoseObject(objectSelected.transform.position);
                }
                if (Input.GetMouseButtonUp(1))
                {
                    objectSelected.gameObject.SetActive(false);
                    objectSelected = null;
                }
            }
            else if(sortSelected!=null)
            {
                Vector3 p1 = masterCamera.transform.position;
                Vector3 p2 = masterCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, masterCamera.farClipPlane));
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
                        sortSelected.getSortVisualScript().updatePosition(rayInfo.collider.gameObject.transform.localPosition);
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
        CmdSetObjectSelected(i, j);
    }

    public int getMana()
    {
        return mana;
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

    [Command]
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
    }

    [Command]
    public void CmdPoseObject(Vector3 pos)
    {
        RpcPoseObject(pos);
        if (!NetworkClient.active)
        {
            objectSelected.PoseObject(pos);
            //objectSelected = null;
        }
    }

    [ClientRpc]
    public void RpcPoseObject(Vector3 pos)
    {
        objectSelected.PoseObject(pos);
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
            if (objectSelected != null)
                objectSelected.gameObject.SetActive(false);
            objectSelected = allContainerScript.getContainer(i).GetChildren()[j];
            objectSelected.gameObject.SetActive(true);
            objectSelected.setLocalPlayerScript(localPlayerScript);
        }
    }

    [ClientRpc]
    public void RpcSetObjectSelected(int i, int j)
    {
        if (objectSelected != null)
            objectSelected.gameObject.SetActive(false);
        objectSelected = allContainerScript.getContainer(i).GetChildren()[j];
        objectSelected.gameObject.SetActive(true);
        objectSelected.setLocalPlayerScript(localPlayerScript);
    }



    public override void RestartPlayer()
    {
       //
    }

    public Transform getRunnerView()
    {
        return runnerView;
    }

}