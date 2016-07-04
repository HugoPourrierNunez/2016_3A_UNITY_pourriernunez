using UnityEngine;
using System.Collections;

/*Sort qui inflige des dégats au joueur*/
public class DamageSortScript : AbstractSortScript
{

    public override bool executeSort(RunnerController player, float timeElapsed)
    {
        float degat = degatPerSec * timeElapsed;
        if (degat > 0)
        {
            player.removePV(degat);
        }

        if (startTime.ContainsKey(player))
        {
            startTime[player] -= timeElapsed;
            if (startTime[player] <= 0)
            {
                startTime.Remove(player);
                return false;
            }
        }


        return true;
    }
}
