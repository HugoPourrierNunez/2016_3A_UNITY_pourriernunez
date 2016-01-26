using UnityEngine;
using System.Collections;

public class RunnerMoveScript : MonoBehaviour {

    [SerializeField]
    Rigidbody myRb;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float jumpSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // Vitesse initiale du personnage
        myRb.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if(Input.GetKey(KeyCode.Q))
        {
            myRb.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            myRb.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //myRb.transform.position += Vector3.up * jumpSpeed * Time.deltaTime;
            myRb.AddRelativeForce(Vector3.up * jumpSpeed * Time.deltaTime);
        }
	}
}
