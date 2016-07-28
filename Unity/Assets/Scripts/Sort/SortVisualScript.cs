using UnityEngine;
using System.Collections;

/*Sort qui gère le retourne visuel qu'à le master lorsqu'il a selectionné un sort*/
public class SortVisualScript : MonoBehaviour
{

    [SerializeField]
    Material materialOK;

    [SerializeField]
    Material materialNotOK;

    [SerializeField]
    Renderer objectRenderer;

    private bool ok=false;

    public void setOK(bool isOK)
    {
         ok = isOK;
         objectRenderer.material = isOK ? materialOK : materialNotOK;
    }

    public void updatePosition(Vector3 pos)
    {
        gameObject.transform.localPosition = pos;
    }

    public bool CanBeActivate()
    {
        return ok;
    }

}
