using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*Classe utilisée par les bouton d'action sur lequel le master appuie pour place un obstacle ou un monstre*/
public class ButtonMasterActionScript : MonoBehaviour {

    [SerializeField]
    Image image;

    [SerializeField]
    Button btn;

    [SerializeField]
    MasterController masterController;

    [SerializeField]
    AllContainerScript allContainerScript;

    [SerializeField]
    int numberContainer;

    private SpawnableObjectContainerScript go=null;

    // Use this for initialization
    void Start () {
        go = allContainerScript.getContainer(numberContainer);
        bool visible = !(go == null || go.isEmpty());
        if(btn!=null)
            btn.enabled = visible;
        if(image!=null)
            image.enabled = visible;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    /*Méthode appelée par le bouton à l'appuie pour dire au mastercontroller qu'il a selectionné l'objet*/
    public void selectObject()
    {
        if(btn.enabled)
        {
            int indice = go.getFirstDisableGOIndice();
            if (masterController.getMana() < go.GetChildren()[indice].getCout())
            {
                WarningMana();
                go.GetChildren()[indice].gameObject.SetActive(false);
            }
            else
                masterController.setObjectSelected(numberContainer, indice);
        }
        
    }

    /*Message de warning si le joueur n'a pas assez de mana*/
    private void WarningMana()
    {
        print("Pas assez de mana");
    }
}
