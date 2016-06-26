using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RunnerListScript : NetworkBehaviour {

    [SerializeField]
    List<RunnerController> runnerList = new List<RunnerController>();

    public int getRunnerIdByView(GameObject go)
    {
        for(int i=0;i<runnerList.Count;i++)
        {
            if (runnerList[i].getView() == go)
            {
                return i;
            }
                
        }
        return -1;
    }

    public RunnerController getRunner(int i)
    {
        if (i < 0 || i >= runnerList.Count)
            return null;
        return runnerList[i];
    }
}
