using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*Classe utilisé pour les barres de progression horizontales*/
public class HBarScript : MonoBehaviour {

    [SerializeField]
    Image image;

    [SerializeField]
    Transform myTransform;

    private Color redColor = Color.red;
    private Color greenColor = Color.green;
    private bool changed = false;
    private float changeColor = .2f;
    private Vector3 scale=new Vector3(1,1,1);

    /*Met à jour le pourcentage de la barre de progression (entre 0 et 1)*/
    public void changePercentage(float percent)
    {
        scale.x = percent;
        myTransform.localScale = scale;
        if (!changed && percent <= changeColor)
        {
            image.color = redColor;
            changed = true;
        }
        else if (changed && percent > changeColor)
        {
            image.color = greenColor;
            changed = false;
        }
            
    }
}
