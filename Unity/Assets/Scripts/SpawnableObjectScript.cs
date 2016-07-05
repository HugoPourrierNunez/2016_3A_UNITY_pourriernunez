using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/*Classe qui sert de classe mère à tout les object qui peuvent être posé sur le terrain par le master*/
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
    int numConteneur;


    [SerializeField]
    protected int indice = -1;

    public void setIndice(int ind)
    {
        indice = ind;
    }

    void OnDisable()
    {
        runnerController.DesactiveObject(numConteneur, indice);
    }

    virtual public void Play()
    {
        //rien
    }

    public void setLocalPlayerScript(LocalPlayerScript lps)
    {
        localPlayerScript = lps;
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
    }

    public bool CanBePosed()
    {
        return canBePosed && !isHide;
    }

    virtual public void Hide()
    {
        if (objectRenderer != null)
            objectRenderer.enabled = false;
        else
            mesh.SetActive(false);
        isHide = true;
    }

    virtual public void UpdatePosition(Vector3 position, float distance)
    {
        effectActive = false;
        if (objectRenderer != null)
            objectRenderer.enabled = true;
        else
            mesh.SetActive(true);
        isHide = false;
        myCollider.gameObject.transform.position = new Vector3(position.x, myCollider.gameObject.transform.position.y, position.z); ;
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
        objectRenderer.enabled = true;
        effectActive = true;
        myCollider.gameObject.transform.position = new Vector3(pos.x, myCollider.gameObject.transform.position.y,pos.z);
        myCollider.gameObject.SetActive(true);
        if (objectRenderer != null)
            objectRenderer.material = normal;
        myCollider.enabled = true;
        runnerController = runnerList.getRunner(runnerInd);
    }
    
    virtual public void Desactive()
    {
        gameObject.SetActive(false);
        effectActive = false;
        myCollider.enabled = false;
    }

}
