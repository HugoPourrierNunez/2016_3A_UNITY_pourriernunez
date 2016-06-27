using UnityEngine;
using System.Collections;

public class DamageSortScript : AbstractSortScript
{

    public override bool executeSort(RunnerController player, float timeElapsed)
    {
        //print("sort execute");
        float degat = degatPerSec * timeElapsed;
        if (degat > 0)
        {
            //print("degats=" + degat);
            player.removePV(degat);
        }

        if (startTime.ContainsKey(player))
        {
            startTime[player] -= timeElapsed;
            if (startTime[player] <= 0)
            {
                startTime.Remove(player);
                print("fin sort");
                return false;
            }
        }


        return true;
    }
}
