using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FireProjectileScript : NetworkBehaviour {

    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    float fireInterval;

    float lastFireTime;

    [SerializeField]
    Transform projectileSpawnPoint;

	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time > lastFireTime + fireInterval)
                {
                    CmdFire();
                    lastFireTime = Time.time;
                }
            }
        }
	}

    [Command]
    void CmdFire()
    {
       var projectile =  (GameObject)GameObject.Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        NetworkServer.Spawn(projectile);
    }
}
