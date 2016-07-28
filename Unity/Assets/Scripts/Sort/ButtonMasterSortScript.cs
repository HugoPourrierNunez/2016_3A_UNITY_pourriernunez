using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*Classe utiliser par les bouton de l'UI du master qui servent à selectionné les sorts*/
public class ButtonMasterSortScript : MonoBehaviour {

    [SerializeField]
    Image image;

    [SerializeField]
    Button btn;

    [SerializeField]
    Text text;

    private MasterController masterController;
    private AbstractSortScript sort=null;
    private int sortNumber = -1;

    public void initButton(AbstractSortScript s, int number, MasterController master)
    {
        masterController = master;
        sort = s;
        text.text = sort.getNameSort();
        sortNumber = number;
        if (btn != null)
            btn.enabled = true;
        if (image != null)
            image.enabled = true;
    }

    public AbstractSortScript getSort()
    {
        return sort;
    }

    public void selectSort()
    {
        if (btn.enabled)
        {
            if (masterController.getMana() < sort.getCout())
            {
                WarningMana();
            }
            else
                masterController.setSortSelected(sortNumber);
        }
    }

    private void WarningMana()
    {
        print("Pas assez de mana");
    }

}
