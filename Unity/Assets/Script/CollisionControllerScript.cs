using UnityEngine;
using System.Collections;

public class CollisionControllerScript : MonoBehaviour {

    [SerializeField]
    AudioSource myShootSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter(Collision col)
    {
        print("Tu as perdu !");
        myShootSound.Play();
    }
}
