using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LevelObstacleScript : NetworkBehaviour {

    [SerializeField]
    LevelGeneratorScript level;

    [SerializeField]
    private int indice;

    [SerializeField]
    Material transparentMaterial;

    [SerializeField]
    Collider myCollider;

    [SerializeField]
    Renderer myRenderer;

    private Material myMaterial;

    public void Start()
    {
        myMaterial = myRenderer.material;
    }

    public void setTransparent(bool isTransparent)
    {
        if(isTransparent)
        {
            myCollider.enabled = false;
            myRenderer.material = transparentMaterial;
        }
        else
        {
            myCollider.enabled = true;
            myRenderer.material = myMaterial;
        }

    }

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
            level.getRunnerController().DesactiveObstacle(indice, gameObject.layer == LayerMask.NameToLayer("ObstacleDestroyable"));
        }
    }
}
