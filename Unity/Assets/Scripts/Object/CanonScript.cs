using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/*Classe qui gère les canon*/
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

    private float op=0, ad=0, angleRot=0;

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
            timerRotation -= interval;
            op = Mathf.Abs(objectToFollow.position.z - transform.position.z);
            ad = Mathf.Abs(objectToFollow.position.x - transform.position.x);
            angleRot = RadianToDegree(Mathf.Atan(op / ad));
            if (objectToFollow.position.z > transform.position.z)
                angleRot -= angleRot * 2;
            if (objectToFollow.position.x < transform.position.x)
                angleRot += (90- angleRot) *2;
            transform.rotation = Quaternion.identity;
            transform.Rotate(Vector3.up*angleRot+Vector3.forward*angleViseur);
        }
        if (timerShot>=intervalTir && effectActive && objectToFollow != null && NetworkServer.active)
        {
            masterController.PlayMonster(numConteneur, indice);
        }


    }

    /*Méthode qui enlève des pv à un joueur touché*/
    public void removePV(GameObject cible)
    {
        if(cible==runnerController.getView().gameObject && NetworkServer.active)
            runnerController.RemovePVNetwork(degat);
    }

    /*Appele une séquence d'action de l'objet, ici un tir*/
    public override void Play()
    {
        base.Play();
        timerShot -= intervalTir;
        BalleScript b = balles.getFirstDisableGO();
        if (b != null)
        {
            b.gameObject.SetActive(true);
            b.startLifeTime(lifeTimeBalle);
            b.transform.localPosition = Vector3.zero;
            b.getRigidBody().velocity = Vector3.zero;
            b.getRigidBody().AddRelativeForce(Vector3.up * forceTire, ForceMode.VelocityChange);
        }
    }

    /*Met à jour la posiition de l'objet*/
    override public void UpdatePosition(Vector3 position, float distance, bool placeOccuped)
    {
        base.UpdatePosition(position,distance, placeOccuped);
    }

    /*Fait spawn l'objet à une position*/
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

    /*Cache l'objet*/
    public override void Hide()
    {
        base.Hide();
        canon.SetActive(false);
    }
}
