using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerScript : NetworkBehaviour
{

    [SerializeField]
    GameObject runner;

    [SerializeField]
    GameObject master;

    [SerializeField]
    GameObject runnerCam;

    [SerializeField]
    GameObject masterCam;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (isLocalPlayer)
        {
            if(transform.position.x == 0)
            {
                runner.gameObject.SetActive(true);
            }
            else
            {
                master.gameObject.SetActive(true);
            }
            
        }
        if (transform.position.x == 0)
        {
            runnerCam.gameObject.SetActive(true);
        }
        else
        {
            masterCam.gameObject.SetActive(true);
        }

        ListScript ob = GameObject.Find("Obstacle").GetComponent<ListScript>();
        ob.setCamera(runnerCam.GetComponent<Camera>());
    }

}
