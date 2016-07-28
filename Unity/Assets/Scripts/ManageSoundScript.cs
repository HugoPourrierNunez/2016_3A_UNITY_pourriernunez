using UnityEngine;
using System.Collections;

public class ManageSoundScript : MonoBehaviour {


    [SerializeField]
    AudioSource soundInGame;

    [SerializeField]
    AudioSource soundGameOver;

    [SerializeField]
    AudioSource soundMenu;

    [SerializeField]
    AudioSource soundCollision;

    public void OnPlaySoundInGame()
    {
        soundInGame.Stop();
        soundInGame.Play();
    }

    public void OnStopSoundInGame()
    {
        soundInGame.Pause();
    }

    public void OnPlaySoundMenu()
    {
        soundMenu.Play();
    }

    public void OnStopSoundMenu()
    {
        soundMenu.Stop();
    }

    public void OnPlaySoundGameOver()
    {
        soundGameOver.Play();
    }

    public void OnPlaySoundCollision()
    {
        soundCollision.Play();
    }
}
