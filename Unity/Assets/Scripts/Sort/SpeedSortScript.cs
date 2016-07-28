using UnityEngine;
using System.Collections;
using System;

public class SpeedSortScript :  AbstractSortScript
{
    [SerializeField]
    float speedCoef;

    public override bool executeSort(RunnerController player, float timeElapsed)
    {
        float degat = degatPerSec * timeElapsed;
        if (degat > 0)
        {
            player.removePV(degat);
        }

        player.changeSpeedNetwork(speedCoef);

        if (startTime.ContainsKey(player))
        {
            startTime[player] -= timeElapsed;
            if (startTime[player] <= 0)
            {
                player.reinitSpeedNetwork();
                startTime.Remove(player);
                return false;
            }
        }


        return true;
    }
}
