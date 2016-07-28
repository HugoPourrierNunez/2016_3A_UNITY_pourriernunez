using UnityEngine;
using System.Collections;

public class TestControl : MonoBehaviour {

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    float vitesseMovement;

    private float vitesseGlobale=2;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Move(Vector3.forward * vitesseMovement);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(Vector3.back * vitesseMovement);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Move(Vector3.left * vitesseMovement);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(Vector3.right * vitesseMovement);
        }
    }

    public void Move(Vector3 translateVector)
    {
        //rb.AddForce(translateVector
        rb.AddForce(translateVector);
        if (rb.velocity.z > vitesseMovement)
            rb.velocity = Vector3.right * rb.velocity.x + Vector3.up * rb.velocity.y + Vector3.forward * vitesseMovement;
        print("velocity=" + rb.velocity);
    }
}
