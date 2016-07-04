using UnityEngine;
using System.Collections;

/*Classe utilisé pour gérer les barres d'avancement verticales*/
public class VBarScript : MonoBehaviour {

    [SerializeField]
    Transform myTransform;

    private Vector3 scale = new Vector3(1, 1, 1);

    /*Change le pourcentage d'avancement de la barre (entre 0 et 1)*/
    public void changePercentage(float percent)
    {
        scale.y = percent;
        myTransform.localScale = scale;
    }
}
