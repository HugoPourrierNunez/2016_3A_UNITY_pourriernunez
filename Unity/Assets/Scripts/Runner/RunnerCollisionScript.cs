using UnityEngine;
using System.Collections;

/*Classe qui sert à gérer la collision du runner avec les différents obstacles*/
public class RunnerCollisionScript : MonoBehaviour {

    private RunnerController runnerController=null;

    void OnCollisionEnter(Collision collision)
    {
        if(runnerController!=null)
        {
            runnerController.Runnercollision(collision);
        }
    }

    public void setRunnerController(RunnerController ctrl)
    {
        runnerController = ctrl;
    }
}
