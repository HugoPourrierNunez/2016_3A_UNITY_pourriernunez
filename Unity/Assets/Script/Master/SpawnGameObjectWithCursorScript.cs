using UnityEngine;
using System.Collections;

public class SpawnGameObjectWithCursorScript : MonoBehaviour {

    [SerializeField]
    Camera myCamera;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Declare a new GameObject
            GameObject newGameObject = new GameObject("MasterGO");
            var ray = myCamera.ScreenPointToRay(Input.mousePosition);

            // Instantiate the GameObject with the position of the cursor
            newGameObject = (GameObject)Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), Input.mousePosition, Quaternion.identity);
        }
	}
}
