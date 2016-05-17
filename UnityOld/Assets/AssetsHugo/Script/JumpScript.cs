using UnityEngine;
using System.Collections;

public class JumpScript : MonoBehaviour {

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    float jumpForce=1f;

    [SerializeField]
    int numberOfJump = 2;

    private int jumpNumber = 0;
    private bool jump = false;

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && jumpNumber < numberOfJump)
        {
            jumpNumber++;
            jump =true;
        }

        if (rb.velocity.y == 0)
            jumpNumber = 0;
        

	}

    void FixedUpdate()
    {
        if (jump)
        {
            jump = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    void Start()
    {
        ListScript ls = GameObject.Find("Obstacle").GetComponent<ListScript>();
        ls.setCamera(this.GetComponent<Camera>());
    }
}
