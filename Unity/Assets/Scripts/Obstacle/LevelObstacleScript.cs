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

    [SerializeField]
    MeshScript meshScript;

    [SerializeField]
    Material myMaterial;

    [SerializeField]
    float y;

    public float getY()
    {
        return y;
    }

    public void setTransparent(bool isTransparent)
    {
        if(isTransparent)
        {
            myCollider.enabled = false;
            if (myRenderer != null)
                myRenderer.material = transparentMaterial;
            else 
                meshScript.setMaterial(transparentMaterial);
        }
        else
        {
            myCollider.enabled = true;
            if (myRenderer != null)
                myRenderer.material = myMaterial;
            else
                meshScript.setMaterial(myMaterial);
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
