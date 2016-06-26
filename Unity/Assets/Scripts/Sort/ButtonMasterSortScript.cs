using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonMasterSortScript : MonoBehaviour {

    [SerializeField]
    Image image;

    [SerializeField]
    Button btn;

    private MasterController masterController;
    private AbstractSortScript sort=null;
    private int sortNumber = -1;

    /*void Start()
    {
        if (btn != null)
            btn.enabled = false;
        if (image != null)
            image.enabled = false;
    }*/

    public void initButton(AbstractSortScript s, int number, MasterController master)
    {
        masterController = master;
        sort = s;
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
        if (masterController.getMana() < sort.getCout())
        {
            WarningMana();
        }
        else
            masterController.setSortSelected(sortNumber);
    }

    private void WarningMana()
    {
        print("Pas assez de mana");
    }

}
