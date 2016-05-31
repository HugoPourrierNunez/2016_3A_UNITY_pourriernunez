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
    float minimumDistanceWithRunner = 0;

    [SerializeField]
    NetworkIdentity id;

    LocalPlayerScript localPlayerScript;

    MasterController ctrl;

    private Material normal;
    private bool isHide = false;

    private bool canBePosed = false;

    public void setLocalPlayerScript(LocalPlayerScript lps)
    {
        localPlayerScript = lps;
        if (lps != null)
            print("local player set");
    }

    public void Start()
    {
        normal = objectRenderer.material;
        if (localPlayerScript == null)
            print("localplayerscript null");
        else
            print("non null");
    }

    public bool CanBePosed()
    {
        return canBePosed && !isHide;
    }

    public void Hide()
    {
        objectRenderer.enabled = false;
        isHide = true;
    }

    public void UpdatePosition(Vector3 position, float distance)
    {
        objectRenderer.enabled = true;
        isHide = false;
        objectRenderer.gameObject.transform.position = position;
        if (distance < minimumDistanceWithRunner)
        {
            canBePosed = false;
            objectRenderer.material = materialNotOK;
        }
        else
        {
            canBePosed = true;
            objectRenderer.material = materialOK;
        }
    }

    public void PoseObject(Vector3 pos)
    {
        myCollider.gameObject.transform.position = pos;
        myCollider.gameObject.SetActive(true);
        objectRenderer.material = normal;
        myCollider.enabled = true;
    }

}
