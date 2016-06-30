using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MenuManagerScript : NetworkBehaviour {

    [SerializeField]
    Canvas startMenu;

    [SerializeField]
    Canvas quitMenu;

    [SerializeField]
    Canvas endMenuMaster;

    [SerializeField]
    Text endMenuMasterText;

    [SerializeField]
    Canvas endMenuRunner;

    [SerializeField]
    Text endMenuRunnerText;

    [SerializeField]
    Button play;

    [SerializeField]
    Button exit;

    [SerializeField]
    Button retry;

    [SerializeField]
    Button exitEnd;

    [SerializeField]
    LocalPlayerScript localPlayerScript;

    [SerializeField]
    MasterUIManagerScript masterUI;

    [SerializeField]
    MasterController masterController;

    [SerializeField]
    RunnerUIManagerScript runner1UI;

    [SerializeField]
    Canvas waitingMenu;

    [SerializeField]
    Canvas masterChoosePlayer;

    private int numberOfPlayers = -1;
    private int numberOfActivePlayers = 0;

    public void Start()
    {
        if (localPlayerScript.localPlayer!=null && localPlayerScript.localPlayer.isServer && localPlayerScript.localPlayer.isClient)
        {
            masterChoosePlayer.gameObject.SetActive(true);
        }
        else
        {
            waitingMenu.gameObject.SetActive(true);
        }
    }

    public void ExitPress()
    {
        quitMenu.gameObject.SetActive(true);
        play.enabled = false;
        exit.enabled = false;
        retry.enabled = false;
        exitEnd.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.gameObject.SetActive(false);
        play.enabled = true;
        exit.enabled = true;
        retry.enabled = true;
        exitEnd.enabled = true;
    }

    public void StartLevel()
    {
        if(NetworkServer.active)
        {
            print("start level");
            masterController.GenerateLevel();
            masterController.ActivePlayers();
        }
        startMenu.gameObject.SetActive(false);
    }

    public void setNumberOfPlayer(int nb)
    {
        numberOfPlayers = nb;
        if (numberOfPlayers == numberOfActivePlayers)
        {
            masterController.updateWaitingMenu();
        }
    }

    public void HideWaitingMenu()
    {
        print("hide waiting menu");
        waitingMenu.gameObject.SetActive(false);
        if (NetworkServer.active)
            startMenu.gameObject.SetActive(true);
    }

    public int getNumberOfPlayer()
    {
        return numberOfPlayers ;
    }

    public void setNumberOfActivePlayers(int nb)
    {
        numberOfActivePlayers=nb;
        print("nb of players actif = " + numberOfActivePlayers);
        if(numberOfPlayers==numberOfActivePlayers)
        {
            masterController.updateWaitingMenu();
        }
    }

    public int getNumberOfActivePlayers()
    {
        return numberOfActivePlayers;
    }

    public void showWaitingMenu()
    {
        masterChoosePlayer.gameObject.SetActive(false);
        if(numberOfPlayers != numberOfActivePlayers)
            waitingMenu.gameObject.SetActive(true);
        else
            startMenu.gameObject.SetActive(true);
    }

    public void StartMenuShow()
    {
        endMenuMaster.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);
        localPlayerScript.localPlayer.controlActivated = false;
        localPlayerScript.localPlayer.RestartPlayer();
    }

    public Canvas getEndMenuRunner()
    {
        return endMenuRunner;
    }

    public void ExitGame()
    {
        localPlayerScript.localPlayer.Quit();
    }

    public void EndLevelShow(bool runnerWin)
    {
        localPlayerScript.localPlayer.controlActivated = false;
        masterUI.gameObject.SetActive(false);
        runner1UI.gameObject.SetActive(false);

        if (localPlayerScript.localPlayer.isServer)
        {
            if (!runnerWin)
                endMenuMasterText.text = "YOU WIN";
            else
                endMenuMasterText.text = "GAME OVER";
            endMenuMaster.gameObject.SetActive(true);
        }
        else
        {
            if (runnerWin)
                endMenuRunnerText.text = "YOU WIN";
            else
                endMenuRunnerText.text = "GAME OVER";
            endMenuRunner.gameObject.SetActive(true);
        }
    }
}
