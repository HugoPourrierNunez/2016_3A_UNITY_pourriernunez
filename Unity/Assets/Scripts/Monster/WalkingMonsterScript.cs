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

    float interval = 1f;
    float nextTime = 0;

    private static int numConteneur=1;
    private float timeEnd;

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
            nextTime += interval;
            

        }
    }

    /*Méthode qui joue une séquence d'action du monstre*/
    override public void Play()
    {
        rb.velocity = Vector3.up * force;
        runnerController.removePV(degat);
    }

    /*Désactive le monstre*/
    public override void Desactive()
    {
        base.Desactive();
        rb.isKinematic = true;
    }

    /*Fonction appelé lorsque l'on fait spawn le monstre*/
    public override void PoseObject(Vector3 pos, int runnerInd)
    {
        base.PoseObject(pos, runnerInd);
        rb.isKinematic = false;
        timeEnd = Time.time + lifeTimeInSecond;

        print("time end =" + timeEnd);
        nextTime = 0;
    }


}
