using UnityEngine;
using System.Collections;

public class CanonFollowRunnerScript : MonoBehaviour {

    [SerializeField]
    Transform runner;

    private float oldAngleX=0;
    private float oldAngleZ = 0;

	// Use this for initialization
	void Start () {
        transform.Rotate(new Vector3(60, 0, 45));
    }
	
	// Update is called once per frame
	void Update () {
        float rotateX = 0;
        float rotateZ = 0;
        //Angle x
        float tanX = Mathf.Abs(runner.position.y - transform.position.y) / Mathf.Abs(runner.position.z - transform.position.z);
        float angleX = (Mathf.Rad2Deg*Mathf.Atan(tanX));

        if (runner.position.z > transform.position.z)
            angleX = 90 - angleX;
        else
            angleX = -90 + angleX;
        if (angleX != oldAngleX)
        {
            rotateX = angleX - oldAngleX;
            oldAngleX = angleX;
        }
        //Angle y
        float tanZ = Mathf.Abs(runner.position.y - transform.position.y) / Mathf.Abs(runner.position.x - transform.position.x);
        float angleZ = (Mathf.Rad2Deg * Mathf.Atan(tanZ));
        if (runner.position.x < transform.position.x)
            angleZ = 90 - angleZ;
        else
            angleZ = -90 + angleZ;
        if (angleZ!= oldAngleZ)
        {
            rotateZ = angleZ - oldAngleZ;
            oldAngleZ = angleZ;
        }

        if(rotateX!=0 || rotateZ!=0)
        {
            transform.Rotate(new Vector3(rotateX, 0, rotateZ));
        }
        
    }
}
