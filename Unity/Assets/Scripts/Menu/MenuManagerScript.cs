using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

/*Classe qui gère la navigation dans les menus du jeu*/
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

    [SerializeField]
    Slider longueurLevelSlider;

    [SerializeField]
    Slider largeurLevelSlider;

    [SerializeField]
    Slider difficultyLevelSlider;

    [SerializeField]
    Slider numberDestroyableObstacleLevelSlider;

    [SerializeField]
    Slider numberUndestroyableObstacleLevelSlider;

    [SerializeField]
    Canvas levelParameterMenu;

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

    /*Méthode qui quitte l'application*/
    public void ExitPress()
    {
        quitMenu.gameObject.SetActive(true);
        play.enabled = false;
        exit.enabled = false;
        retry.enabled = false;
        exitEnd.enabled = false;
    }

    /*Méthode qui cache le menu 'quitter'*/
    public void NoPress()
    {
        quitMenu.gameObject.SetActive(false);
        play.enabled = true;
        exit.enabled = true;
        retry.enabled = true;
        exitEnd.enabled = true;
    }

    /*Méthode appelé au start level*/
    public void StartLevel()
    {
        if(NetworkServer.active)
        {
            print("start level");
            masterController.GenerateLevel(longueurLevelSlider.value, 
                largeurLevelSlider.value, 
                difficultyLevelSlider.value, 
                numberDestroyableObstacleLevelSlider.value,
                numberUndestroyableObstacleLevelSlider.value);
            masterController.ActivePlayers();
        }
        startMenu.gameObject.SetActive(false);
    }

    /*Met à jour le nombre de player voulu sur la partie*/
    public void setNumberOfPlayer(int nb)
    {
        numberOfPlayers = nb;
        if (numberOfPlayers == numberOfActivePlayers)
        {
            masterController.updateWaitingMenu();
        }
    }

    /*Cache le menu d'attente de connection des joueurs*/
    public void HideWaitingMenu()
    {
        print("hide waiting menu");
        waitingMenu.gameObject.SetActive(false);
        if (NetworkServer.active)
            levelParameterMenu.gameObject.SetActive(true);
            //startMenu.gameObject.SetActive(true);
    }

    /*Renvoie le nombre de joueurs voulu sur la partie*/
    public int getNumberOfPlayer()
    {
        return numberOfPlayers ;
    }
    
    /*Met à jour le nombre de joueurs actifs sur la partie*/
    public void setNumberOfActivePlayers(int nb)
    {
        numberOfActivePlayers=nb;
        print("nb of players actif = " + numberOfActivePlayers);
        if(numberOfPlayers==numberOfActivePlayers)
        {
            masterController.updateWaitingMenu();
        }
    }

    /*Retourne le nombre de joueurs voulus sur la partie*/
    public int getNumberOfActivePlayers()
    {
        return numberOfActivePlayers;
    }

    /*Affiche le menu d'attente de connection des autres joueurs*/
    public void showWaitingMenu()
    {
        masterChoosePlayer.gameObject.SetActive(false);
        if(numberOfPlayers != numberOfActivePlayers)
            waitingMenu.gameObject.SetActive(true);
        else
            startMenu.gameObject.SetActive(true);
    }

    /*Affiche le menu de début de partie, ici le menu pour paramètrer le level*/
    public void StartMenuShow()
    {
        endMenuMaster.gameObject.SetActive(false);
        levelParameterMenu.gameObject.SetActive(true);
        localPlayerScript.localPlayer.controlActivated = false;
        localPlayerScript.localPlayer.RestartPlayer();
    }

    /*Cache le menu de paramètrage du level*/
    public void HideLevelParameterMenu()
    {
        levelParameterMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);
    }

    /*Renvoie le menu de fin de partie*/
    public Canvas getEndMenuRunner()
    {
        return endMenuRunner;
    }

    /*Affiche un petit menu qui demande si on veut vraiment quitter le jeu*/
    public void ExitGame()
    {
        localPlayerScript.localPlayer.Quit();
    }

    /*Affiche le menu de fin de partie*/
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
