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
        
    }

    public void EndLevelShow()
    {
        localPlayerScript.localPlayer.controlActivated = false;
        endMenu.gameObject.SetActive(true);
    }
}
