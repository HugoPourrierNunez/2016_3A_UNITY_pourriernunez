using UnityEngine;
using System.Collections;

public class MasterClickScript : MonoBehaviour {

    [SerializeField]
    Transform target;

    [SerializeField]
    Camera cam;

    private GameObject obj = null;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 250))
            {
                if (hit.transform.CompareTag(target.transform.tag))
                {
                    //do whatever......
                    print("ok");
                    Debug.Log(hit.point);
                    if(obj!=null)
                    {
                        Instantiate(obj, hit.point, Quaternion.identity);
                        print("Instanciate");
                    }
                        
                }
            }
        }
    }

    public void setObj(GameObject o)
    {
        obj = o;
    }
}
