using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class RunnerMoveScript : NetworkBehaviour
{

    [SerializeField]
    Rigidbody myRb;

    [SerializeField]
    float moveSpeedParam;

    [SerializeField]
    AudioSource myShootSound;

    [SerializeField]
    float jumpSpeed;

    //[SerializeField]
    //Canvas myMenu;

    [SerializeField]
    GameObject goRunner;

    private int i;
    private float moveSpeed;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            if (goRunner.active == true)
            {
                moveSpeed = 10;
            }
            else
            {
                moveSpeed = 0;
            }
        }
	}

    public void SetMoveSpeedRunner()
    {
        if (isLocalPlayer)
        {
            print("test");
            myShootSound.Play();
            moveSpeed = moveSpeedParam;

        }
    }
	
	// Update is called once per frame
	void Update () {

        if (isLocalPlayer)
        {
            // Vitesse initiale du personnage
            myRb.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Q))
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

    /*void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Sol" && col.gameObject.tag != "Wall")
        {
            moveSpeed = 0;
            myMenu.gameObject.SetActive(true);
        }

        i++;
    }*/
}
