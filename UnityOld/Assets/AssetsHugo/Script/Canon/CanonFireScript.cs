using UnityEngine;
using System.Collections;

public class CanonFireScript : MonoBehaviour {

    [SerializeField]
    float cadence;

    [SerializeField]
    float vitesseBalle;

    [SerializeField]
    float distanceBalle;

    [SerializeField]
    Transform tr;

    [SerializeField]
    float maxAngle;

    [SerializeField]
    float vitesseRotation;

    [SerializeField]
    ListBalleScript script;

    private bool plus=true;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Rad2Deg*tr.rotation.z > maxAngle && plus)
        {
            plus = false;
            vitesseRotation = -vitesseRotation;
        }
        else if( Mathf.Rad2Deg * tr.rotation.z < -maxAngle && !plus)
        {
            plus = true;
            vitesseRotation = -vitesseRotation;
        }
            
            
        tr.Rotate(new Vector3(0,0, vitesseRotation) *Time.deltaTime);

        script.activerBalle(tr.position,tr.rotation,new Vector3(0, 1, 0)*vitesseBalle);


	}
}
