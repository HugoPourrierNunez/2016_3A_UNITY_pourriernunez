using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    [SerializeField]
    MasterClickScript script;

    [SerializeField]
    GameObject obj;

    private bool selected=false;

	public void buttonIsSelected(bool b)
    {
        if (b && !selected)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            selected = true;
        }

        else
        {
            this.GetComponent<UnityEngine.UI.Image>().color = Color.white;
            selected = false;
        }
    }

    public void updateMasterObject()
    {
        if(selected)
            script.setObj(obj);
        else script.setObj(null);
    }
}
