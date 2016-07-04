using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CanonScript : SpawnableObjectScript {

    [SerializeField]
    float angleViseur = -70;

    [SerializeField]
    float interval=.1f;

    [SerializeField]
    float intervalTir = 1;

    [SerializeField]
    float forceTire = 1500;

    [SerializeField]
    GameObject canon;

    [SerializeField]
    BallesContainerScript balles;

    [SerializeField]
    float lifeTimeBalle = 2;

    [SerializeField]
    float degat = 5;

    private float timerRotation = 0;
    private float timerShot = 0;
    private int numConteneur = 2;

    private float op=0, ad=0, angleRot=0/*, angleElev*/;

    [SerializeField]
    Transform objectToFollow = null;

    void OnDisable()
    {
        runnerController.DesactiveObject(numConteneur, indice);
    }

    // Update is called once per frame
    void Update () {
        timerRotation += Time.deltaTime;
        timerShot += Time.deltaTime;
        if (timerRotation>=interval && effectActive && objectToFollow!=null)
        {
            //print("rotation ");
            timerRotation -= interval;
            op = Mathf.Abs(objectToFollow.position.z - transform.position.z);
            ad = Mathf.Abs(objectToFollow.position.x - transform.position.x);
            angleRot = RadianToDegree(Mathf.Atan(op / ad));
            if (objectToFollow.position.z > transform.position.z)
                angleRot -= angleRot * 2;
            if (objectToFollow.position.x < transform.position.x)
                angleRot += (90- angleRot) *2;
            /*if(objectToFollow.position.x!=transform.position.x)
                angleElev = RadianToDegree(Mathf.Atan(Mathf.Abs(objectToFollow.position.y - transform.position.y) / Mathf.Abs(objectToFollow.position.x - transform.position.x)));
            else
                angleElev = RadianToDegree(Mathf.Atan(Mathf.Abs(objectToFollow.position.y - transform.position.y) / Mathf.Abs(objectToFollow.position.z - transform.position.z)));*/
            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0, angleRot, angleViseur/*+angleElev*/));
        }
        if (timerShot>=intervalTir && effectActive && objectToFollow != null && NetworkServer.active)
        {
            masterController.PlayMonster(numConteneur, indice);
        }


    }

    public void removePV(GameObject cible)
    {
        if(cible==runnerController.getView().gameObject && NetworkServer.active)
            runnerController.RemovePVNetwork(degat);
    }

    public override void Play()
    {
        base.Play();
        timerShot -= intervalTir;
        BalleScript b = balles.getFirstDisableGO();
        if (b != null)
        {
            b.gameObject.SetActive(true);
            b.startLifeTime(lifeTimeBalle);
            b.transform.localPosition = new Vector3(0, 0, 0);
            b.getRigidBody().velocity = Vector3.zero;
            b.getRigidBody().AddRelativeForce(Vector3.up * forceTire, ForceMode.VelocityChange);
        }
    }

    override public void UpdatePosition(Vector3 position, float distance)
    {
        base.UpdatePosition(position,distance);
    }

    public override void PoseObject(Vector3 pos, int runnerInd)
    {
        base.PoseObject(pos, runnerInd);
        canon.SetActive(true);
        objectToFollow = runnerController.getView().transform;
    }

    private float RadianToDegree(float angle)
    {
        return angle * (180.0f / Mathf.PI);
    }

    public override void Hide()
    {
        base.Hide();
        canon.SetActive(false);
    }
}
