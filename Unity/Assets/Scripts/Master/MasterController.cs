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

    private Vector3 positionCamera=new Vector3();
    private Vector3 translationCamera = new Vector3(0, 0, 0);
    private float effectiveZoom = 0;
    private float alignementGauche;
    private SpawnableObjectScript objectSelected = null;

    public void setObjectSelected(SpawnableObjectScript obj)
    {
        if (objectSelected != null)
            objectSelected.gameObject.SetActive(false);
        objectSelected = obj;
    }

    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (Camera.main && Camera.main.gameObject)
        {
            Camera.main.gameObject.SetActive(false);
        }
        masterCamera.gameObject.SetActive(true);

        alignementGauche = getAlignGauche();
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
                Vector3 p1 = masterCamera.transform.position;
                Vector3 p2 = masterCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, masterCamera.farClipPlane));
                RaycastHit rayInfo;
                if (Physics.Linecast(p1, p2, out rayInfo))
                {
                    if (rayInfo.collider.gameObject == floor.gameObject)
                    {
                        p1.x = Mathf.Round(rayInfo.point.x);
                        p1.y = Mathf.Round(rayInfo.point.y);
                        p1.z = Mathf.Round(rayInfo.point.z-.5f) + .5f;
                        objectSelected.UpdatePosition(p1, Vector3.Distance(p1, runnerView.position));
                    }
                    else objectSelected.Hide();
                }
                else objectSelected.Hide();
                if(objectSelected.CanBePosed() && Input.GetMouseButtonUp(0))
                {
                    objectSelected.PoseObject();
                    objectSelected = null;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    objectSelected.gameObject.SetActive(false);
                    objectSelected = null;
                }
            }
        }
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