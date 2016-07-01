using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnableObjectScript : NetworkBehaviour {

    [SerializeField]
    Collider myCollider;

    [SerializeField]
    Material materialOK;

    [SerializeField]
    Material materialNotOK;

    [SerializeField]
    Renderer objectRenderer;

    [SerializeField]
    GameObject mesh;

    [SerializeField]
    protected float minimumDistanceWithRunner = 0;

    [SerializeField]
    NetworkIdentity id;

    [SerializeField]
    int cout = 5;

    [SerializeField]
    LocalPlayerScript localPlayerScript;

    [SerializeField]
    protected MasterController masterController;

    [SerializeField]
    RunnerListScript runnerList;

    private Material normal;
    private bool isHide = false;

    private bool canBePosed = false;

    protected bool effectActive = false;

    protected RunnerController runnerController;

    [SerializeField]
    protected int indice = -1;

    public void setIndice(int ind)
    {
        indice = ind;
    }

    virtual public void Play()
    {
        //rien
    }

    public void setLocalPlayerScript(LocalPlayerScript lps)
    {
        localPlayerScript = lps;
        /*if (lps != null)
            print("local player set");*/
    }

    public void setRunnerList(RunnerListScript runner)
    {
        runnerList = runner;
    }

    public void setMasterController(MasterController ctrl)
    {
        masterController = ctrl;
    }

    public void Start()
    {
        if (objectRenderer != null)
            normal = objectRenderer.material;
        /*if (localPlayerScript == null)
            print("localplayerscript null");
        else
            print("non null");*/
    }

    public bool CanBePosed()
    {
        return canBePosed && !isHide;
    }

    public void Hide()
    {
        if (objectRenderer != null)
            objectRenderer.enabled = false;
        else
            mesh.SetActive(false);
        isHide = true;
    }

    public void UpdatePosition(Vector3 position, float distance)
    {
        effectActive = false;
        if (objectRenderer != null)
            objectRenderer.enabled = true;
        else
            mesh.SetActive(true);
        isHide = false;
        myCollider.gameObject.transform.position = position;
        if (distance < minimumDistanceWithRunner)
        {
            canBePosed = false;
            if(objectRenderer!=null)
                objectRenderer.material = materialNotOK;
        }
        else
        {
            canBePosed = true;
            if (objectRenderer != null)
                objectRenderer.material = materialOK;
        }
    }

    public int getCout()
    {
        return cout;
    }

    virtual public void PoseObject(Vector3 pos,int runnerInd)
    {
        effectActive = true;
        myCollider.gameObject.transform.position = pos;
        myCollider.gameObject.SetActive(true);
        if (objectRenderer != null)
            objectRenderer.material = normal;
        myCollider.enabled = true;
        runnerController = runnerList.getRunner(runnerInd);
        if (runnerController == null)
            print("non find runner num:" + runnerInd);
    }
    
    public void Desactive()
    {
        print("desactive");
        gameObject.SetActive(false);
        effectActive = false;
        myCollider.enabled = false;
    }

}
