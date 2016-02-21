using UnityEngine;
using System.Collections;

public class MasterPositionScript : MonoBehaviour {

    [SerializeField]
    Transform runner;

	// Use this for initialization
	void Start () {
	
	}


	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.position = new Vector3(runner.position.x - 7f, runner.position.y+13f, runner.position.z);
	}

    public void setRunner(Transform tr)
    {
        runner = tr;
    }
}
