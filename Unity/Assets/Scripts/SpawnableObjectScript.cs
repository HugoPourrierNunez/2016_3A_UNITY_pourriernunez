using UnityEngine;
using System.Collections;

public class SpawnableObjectScript : MonoBehaviour {

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

    MasterController ctrl;

    private Material normal;
    private bool isHide = false;

    private bool canBePosed = false;

    public void Start()
    {
        normal = objectRenderer.material;
    }

    public bool CanBePosed()
    {
        return canBePosed && isHide;
    }

    public void Hide()
    {
        objectRenderer.enabled = false;
        isHide = true;
    }

    public void UpdatePosition(Vector3 position, float distance)
    {
        objectRenderer.enabled = true;
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

    public void PoseObject()
    {
        objectRenderer.material = normal;
        myCollider.enabled = true;
    }
}
