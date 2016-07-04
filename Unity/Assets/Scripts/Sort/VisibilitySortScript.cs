using UnityEngine;

/*Sort qui affecte la visibilité du joueur*/
public class VisibilitySortScript : AbstractSortScript
{
    [SerializeField]
    private float intensityLight;

    public override bool executeSort(RunnerController player, float timeElapsed)
    {

        if (startTime.ContainsKey(player))
        {
            startTime[player] -= timeElapsed;

            player.getLight().intensity = intensityLight + (1 - intensityLight) * (totalTime - startTime[player]) / totalTime;

            if (startTime[player] <= 0)
            {
                player.getLight().intensity = 1;
                startTime.Remove(player);
                print("fin sort");
                return false;
            }
        }


        return true;
    }
}
