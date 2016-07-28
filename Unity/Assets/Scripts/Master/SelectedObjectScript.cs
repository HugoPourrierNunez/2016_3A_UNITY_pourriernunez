using UnityEngine;
using System.Collections;

public class SelectedObjectScript : MonoBehaviour {

    private SpawnableObjectScript selectedObject = null;

    public void setSelectedObject(SpawnableObjectScript obj)
    {
        selectedObject = obj;
    }


    public SpawnableObjectScript getSelectedObject()
    {
        return selectedObject;
    }
}
