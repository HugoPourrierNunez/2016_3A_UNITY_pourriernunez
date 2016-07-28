using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ProjectileScript :NetworkBehaviour {

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Transform tr;

    [SerializeField]
    float projectileInitialSpeed;

	// Use this for initialization
	void Start () {
        rb.AddRelativeForce(Vector3.forward * projectileInitialSpeed, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
