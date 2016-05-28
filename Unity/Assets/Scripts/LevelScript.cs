using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

    [SerializeField]
    Transform wallLeft;

    [SerializeField]
    Transform wallRight;

    public void activate()
    {
        wallLeft.gameObject.SetActive(true);
        wallRight.gameObject.SetActive(true);
    }
}
