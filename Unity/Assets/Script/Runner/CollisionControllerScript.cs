using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollisionControllerScript : MonoBehaviour {

    [SerializeField]
    AudioSource myShootSound;

    [SerializeField]
    Text myScore;

    
    int i = 0;
    float timeLose;
    float timeStart;

	// Use this for initialization
	void Start () 
    {
        timeStart = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    void OnCollisionEnter(Collision col)
    {
        if (i > 0)
        {
            print("Tu as perdu !");
            myShootSound.Play();
            timeLose = Time.deltaTime;
            float result = timeLose - timeStart;
            myScore.text = "Score : "+result;
        }

        i++;
    }
}
