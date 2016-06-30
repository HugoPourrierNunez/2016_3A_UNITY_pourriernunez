using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WalkingMonsterScript : SpawnableObjectScript {

    [SerializeField]
    float force=5;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    float lifeTimeInSecond = 10;

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

    override public void Play()
    {
        rb.velocity = Vector3.up * force;
    }

    public override void PoseObject(Vector3 pos)
    {
        base.PoseObject(pos);
        timeEnd = Time.time + lifeTimeInSecond;

        print("time end =" + timeEnd);
        nextTime = 0;
    }

}
