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
    Canvas endMenu;

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
    RunnerUIManagerScript runner1UI;

    [SerializeField]
    Canvas waitingMenu;

    [SerializeField]
    Canvas masterChoosePlayer;

    private int numberOfPlayers = 0;
    private int numberOfActivePlayers = 0;

    public void Start()
    {
        if (localPlayerScript.localPlayer.isServer && localPlayerScript.localPlayer.isClient)
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
        startMenu.gameObject.SetActive(false);
        localPlayerScript.localPlayer.controlActivated = true;
        masterUI.gameObject.SetActive(true);
        runner1UI.gameObject.SetActive(true);
    }

    public void setNumberOfPlayer(int nb)
    {
        numberOfPlayers = nb;
    }

    public int getNumberOfPlayer()
    {
        return numberOfPlayers ;
    }

    public void setNumberOfActivePlayers(int nb)
    {
        numberOfActivePlayers=nb;
        print("nb of players actif = " + numberOfActivePlayers);
        if(numberOfPlayers==numberOfActivePlayers && waitingMenu.gameObject.active)
        {
            waitingMenu.gameObject.SetActive(false);
            startMenu.gameObject.SetActive(true);
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
        endMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);
        localPlayerScript.localPlayer.controlActivated = false;
        localPlayerScript.localPlayer.RestartPlayer();
    }

    public void ExitGame()
    {
        localPlayerScript.localPlayer.Quit();
    }

    public void EndLevelShow()
    {
        localPlayerScript.localPlayer.controlActivated = false;
        endMenu.gameObject.SetActive(true);
        masterUI.gameObject.SetActive(false);
        runner1UI.gameObject.SetActive(false);
    }
}
