using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/*Classe qui gère un montre terrestre*/
public class WalkingMonsterScript : SpawnableObjectScript {

    [SerializeField]
    float force=5;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    float lifeTimeInSecond = 10;

    [SerializeField]
    float degat = 5;

    [SerializeField]
    float vitesse = 3;

    float interval = .1f;
    float nextTime = 0;

    private static int numConteneur=1;
    private float timeEnd;
    private bool right=true;

    // Use this for initialization
    void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= nextTime)
        {
            if(NetworkServer.active && effectActive)
            {
                masterController.PlayMonster(numConteneur,indice);
                if (timeEnd <= Time.time)
                {
                    masterController.DesactiveMonster(numConteneur, indice);
                }
            }
            nextTime = Time.time+interval;
            

        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name!="floor" && !col.gameObject.CompareTag("RunnerView"))
        {
            right = !right;
        }
        if(col.gameObject.CompareTag("RunnerView"))
        {
            runnerController.removePV(degat);
            masterController.DesactiveMonster(numConteneur, indice);
        }
    }

    /*Méthode qui joue une séquence d'action du monstre*/
    override public void Play()
    {
        //rb.velocity = Vector3.up * force;
        //print("play");
        if (right)
            rb.velocity = Vector3.right*vitesse;
        else
            rb.velocity = Vector3.left * vitesse;
        //new Vector3(runnerController.getView().transform.position.x
        //runnerController.removePV(degat);
    }

    /*Désactive le monstre*/
    public override void Desactive()
    {
        base.Desactive();
    }

    /*Fonction appelé lorsque l'on fait spawn le monstre*/
    public override void PoseObject(Vector3 pos, int runnerInd)
    {
        base.PoseObject(pos, runnerInd);
        rb.isKinematic = false;
        timeEnd = Time.time + lifeTimeInSecond;
        nextTime = 0;
    }


}
