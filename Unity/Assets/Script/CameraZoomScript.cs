using UnityEngine;
using System.Collections;

public class CameraZoomScript : MonoBehaviour {

    [SerializeField]
    float maxZoom;

    [SerializeField]
    float minZoom;

    [SerializeField]
    float zoomSpeed=1;

    // Update is called once per frame

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.z > minZoom) // back
        {
            transform.position -= new Vector3(0, -1, 1)*zoomSpeed;
            Debug.Log("test");

        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.z<maxZoom) // forward
        {
            transform.position += new Vector3(0, -1, 1)*zoomSpeed;
        }
    }
}
