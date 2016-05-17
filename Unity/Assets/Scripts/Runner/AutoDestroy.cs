using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public void Awake()
    {
        Destroy(gameObject);
    }
}
