using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

/*Classe abstraite qui défini les méthodes de tout les sort*/
public abstract class AbstractSortScript : NetworkBehaviour {

    [SerializeField]
    protected float degatPerSec;

    [SerializeField]
    protected float cost;

    [SerializeField]
    protected float totalTime;

    [SerializeField]
    SortVisualScript sortVisualScript;

    [SerializeField]
    RunnerListScript runnerListScript;

    protected int number = 0;

    protected Dictionary<RunnerController, float> startTime = new Dictionary<RunnerController, float>();

    public abstract bool executeSort(RunnerController player, float timeElapsed);

    [SerializeField]
    string nameSort="Default sort";

    /*Retourne le non du sort*/
    virtual public string getNameSort()
    {
        return nameSort;
    }

    /*Lance le sort sur un runner*/
    public bool lauchSort(int runnerInd)
    {
        RunnerController runner = runnerListScript.getRunner(runnerInd);

        if (startTime.ContainsKey(runner))
            return false;
        
        startTime.Add(runner, totalTime);
        runner.addEffectiveSort(number);
        return true;
    }

    public float getDegatPerSec()
    {
        return degatPerSec;
    }

    public float getTotalTime()
    {
        return totalTime;
    }

    public float getCout()
    {
        return cost;
    }

    public SortVisualScript getSortVisualScript()
    {
        return sortVisualScript;
    }

    public void setNumber(int nb)
    {
        number = nb;
    }

    public int getNumber()
    {
        return number;
    }

    /*Enleve le runner de la liste des runner sur lequel le sort est actif*/
    public void removePlayer(RunnerController player)
    {
        if(startTime.ContainsKey(player))
            startTime.Remove(player);
    }

    public bool affectAlreadyRunner(int i)
    {
        return startTime.ContainsKey(runnerListScript.getRunner(i));
    }
}
