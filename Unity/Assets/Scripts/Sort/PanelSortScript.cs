using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelSortScript : MonoBehaviour {

    [SerializeField]
    List<ButtonMasterSortScript> sortButton;

    [SerializeField]
    SortContainerScript sortContainer;

    [SerializeField]
    MasterController masterController;

    void Start()
    {
        List<AbstractSortScript> sortList  = sortContainer.GetChildren();
        for(int i=0;i<sortList.Count && i<sortButton.Count;i++)
        {
            sortButton[i].initButton(sortList[i],i,masterController);
        }
    }

    public AbstractSortScript getSort(int i)
    {
        if (i < 0 || i >= sortContainer.GetChildren().Count)
            return null;
        return sortContainer.GetChildren()[i];
    }
}
