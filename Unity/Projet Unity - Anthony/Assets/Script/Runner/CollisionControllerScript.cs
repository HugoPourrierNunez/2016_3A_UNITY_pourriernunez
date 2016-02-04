using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollisionControllerScript : MonoBehaviour {

    [SerializeField]
    Text myDistance;

    [SerializeField]
    Text myScore;

    [SerializeField]
    Transform spawnRunner;
    
    int i = 0;
    private float distance;
    private float result;
    private static float myRes;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(transform.position, spawnRunner.position);
        myDistance.text = "Distance : " + distance;
	}


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Sol" && col.gameObject.tag != "Wall")
        {
            print("Tu as perdu !");
            result = distance;
            transform.Translate(Vector3.back * result);
            print("Ton score est de  : " + result);
            myRes = result;
            Application.LoadLevel(0);
        }

        i++;
    }

    void OnLevelWasLoaded()
    {
        myScore.text = "Ton score est de  : " + myRes;
    }
}
