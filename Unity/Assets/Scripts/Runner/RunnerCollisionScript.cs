using UnityEngine;
using System.Collections;

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
