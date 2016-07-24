using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LevelObstacleScript : NetworkBehaviour {

    [SerializeField]
    LevelGeneratorScript level;

    [SerializeField]
    private int indice;

    public void setLevel(LevelGeneratorScript l)
    {
        level = l;
    }

    public void setIndice(int i)
    {
        indice = i;
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("RunnerView"))
        {
            gameObject.SetActive(false);
            if(gameObject.layer == LayerMask.NameToLayer("ObstacleDestroyable"))
                level.DesactiveDestroyableObstacle(indice);
            else
                level.DesactiveUndestroyableObstacle(indice);
        }
    }
}
