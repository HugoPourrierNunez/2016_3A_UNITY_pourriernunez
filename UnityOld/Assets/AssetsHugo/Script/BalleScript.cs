using UnityEngine;
using System.Collections;

public class BalleScript : MonoBehaviour {

    [SerializeField]
    GameObject balle;

    void OnTriggerEnter(Collider other)
    {
        balle.active = false;
    }
}
