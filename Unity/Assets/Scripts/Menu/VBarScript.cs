using UnityEngine;
using System.Collections;

public class VBarScript : MonoBehaviour {

    [SerializeField]
    Transform myTransform;

    private Vector3 scale = new Vector3(1, 1, 1);

    public void changePercentage(float percent)
    {
        scale.y = percent;
        myTransform.localScale = scale;
    }
}
