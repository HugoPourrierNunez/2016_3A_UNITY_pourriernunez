using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GoToPositionScript : NetworkBehaviour {

    [SerializeField]
    Camera tankCamera;

    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    Collider terrainCollider;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (isLocalPlayer)
        {
            if (terrainCollider == null)
            {
                terrainCollider = GameObject.Find("Terrain").GetComponent<TerrainCollider>();
            }
        }
    }
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            tankCamera.gameObject.SetActive(true);
            if (Input.GetMouseButton(1))
            {
                var ray = tankCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (terrainCollider.Raycast(ray, out hitInfo, 1000f))
                {
                    agent.SetDestination(hitInfo.point);
                }
            }
        }
	}
}
