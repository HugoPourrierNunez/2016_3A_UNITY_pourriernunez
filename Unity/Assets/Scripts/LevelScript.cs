using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

    [SerializeField]
    Transform wallLeft;

    [SerializeField]
    Transform wallRight;

    [SerializeField]
    Transform floor;

    public void activate()
    {
        wallLeft.gameObject.SetActive(true);
        wallRight.gameObject.SetActive(true);
    }

    public Transform getFloor()
    {
        return floor;
    }
}
