using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour
{

    [SerializeField]
    Transform tr;

    [SerializeField]
    float speed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            tr.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            tr.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            tr.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            tr.position += Vector3.back * speed * Time.deltaTime;
        }
    }
}
