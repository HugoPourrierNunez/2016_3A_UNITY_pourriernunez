using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour
{

    [SerializeField]
    Transform characterTransform;

    [SerializeField]
    float speed;

    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float vitesseRotation = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            /*if (characterTransform.rotation.y != cameraTransform.rotation.y)
                characterTransform.Rotate(0, cameraTransform.eulerAngles.y, 0);*/
           //Reste encore à faire la rotation du perso en fonction de la camera
            //characterTransform.Rotate(cameraTransform.rotation.x, 0, cameraTransform.eulerAngles.z);
            characterTransform.position += new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z) * speed * Time.deltaTime;
            //characterTransform.Rotate(0, cameraTransform.rotation.y, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            characterTransform.position += characterTransform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            characterTransform.position -= characterTransform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            characterTransform.position -= characterTransform.forward * speed * Time.deltaTime;
        }
    }
}
