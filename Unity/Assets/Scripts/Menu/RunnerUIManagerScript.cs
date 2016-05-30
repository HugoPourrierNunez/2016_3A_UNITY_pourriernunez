using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunnerUIManagerScript : MonoBehaviour {

    [SerializeField]
    HBarScript pvBar;

    [SerializeField]
    VBarScript avancementBar;

    public HBarScript getPvBar()
    {
        return pvBar;
    }

    public VBarScript getavancementBar()
    {
        return avancementBar;
    }
}
