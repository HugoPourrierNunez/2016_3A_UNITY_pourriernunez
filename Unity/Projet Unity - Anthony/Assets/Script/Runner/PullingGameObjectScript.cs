using UnityEngine;
using System.Collections;

public class PullingGameObjectScript : MonoBehaviour {

    [SerializeField]
    Renderer myRenderer;

    [SerializeField]
    Camera myCamera;

    [SerializeField]
    Rigidbody myRb;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(!IsVisibleFrom(myRenderer, myCamera))
        {
            myRenderer.enabled = false;
            myRb.position += Vector3.back * 1600 * Time.deltaTime;
            myRenderer.enabled = true;
        }
	}

    public bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
