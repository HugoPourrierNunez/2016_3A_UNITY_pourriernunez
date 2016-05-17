using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class SpawnGameObjectWithCursorScript : NetworkBehaviour {

    [SerializeField]
    Camera myCamera;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            var screenPos = Input.mousePosition;
            screenPos.z = 20;
            var worldPos = myCamera.ScreenToWorldPoint(screenPos);
            GameObject newGameObject = (GameObject)Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), worldPos, Quaternion.identity);
        }
	}
}
