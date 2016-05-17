using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class StartMenu : NetworkBehaviour {

    [SerializeField]
    Canvas startMenu;

    [SerializeField]
    Canvas quitMenu;

    [SerializeField]
    Button play;

    [SerializeField]
    Button exit;

    [SerializeField]
    LocalPlayerScript localPlayerScript;

    public void ExitPress()
    {
        quitMenu.gameObject.SetActive(true);
        play.enabled = false;
        exit.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.gameObject.SetActive(false);
        play.enabled = true;
        exit.enabled = true;
    }

    public void StartLevel()
    {
        startMenu.gameObject.SetActive(false);
        localPlayerScript.localPlayer.controlActivated = true;
    }

    public void ExitGame()
    {
        
    }
}
