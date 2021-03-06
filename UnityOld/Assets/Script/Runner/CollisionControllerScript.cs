﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CollisionControllerScript : MonoBehaviour
{

    /*[SerializeField]
    Text myDistance;

    [SerializeField]
    Text myScore;*/

    [SerializeField]
    RectTransform barreDeVie;
    
    int i = 0;
    private float distance;
    private float result;
    private static float myRes;
    private float vie ;

    private Transform spawnRunner;

    // Use this for initialization
    void Start () 
    {
        spawnRunner = transform;
        vie = barreDeVie.sizeDelta.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(transform.position, spawnRunner.position);
        //myDistance.text = "Distance : " + distance;
	}


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Sol" && col.gameObject.tag != "Wall")
        {
            vie -= 10;
            barreDeVie.sizeDelta = new Vector2(vie,barreDeVie.sizeDelta.y);
            if(vie<=0)
            {
                print("Tu as perdu !");
                result = distance;
                transform.Translate(Vector3.back * result);
                print("Ton score est de  : " + result);
                myRes = result;
                SceneManager.LoadScene("InGameRunnerOnline");
            }
        }

        i++;
    }

    void OnLevelWasLoaded()
    {
        //myScore.text = "Ton score est de  : " + myRes;
    }
}
