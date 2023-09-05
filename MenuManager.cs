using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public AudioManagerScript audioManagerScript;

    //izlazak iz igre
    public void ExitGame()     
    {
        PlayerPrefs.DeleteKey("Lives");
        Application.Quit();
        Debug.Log("Game Closed");
    }

    //pocetak igre
    public void StartGame()     
    {
        PlayerPrefs.DeleteKey("Lives");
        SceneManager.LoadScene("Level01");
        
    }

    //prelayak na scenu Leaderboard
    public void OpenLeaderboard()     
    {
        SceneManager.LoadScene("LeaderboardScene");
    }

    //prelazak na glavni izbornik
    public void MainMenuUI()    
    {
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.DeleteKey("Lives");
    }

    //prelazak na scenu About
    public void About()    
    {
        SceneManager.LoadScene("About");
    }

    //ukljuci/isljuci zvuk bg i promjena slike
    public void ToggleAudio()
    {
        AudioManagerScript.Instance.ToggleBackgroundMusic(!AudioManagerScript.Instance.isBackgroundMusicEnabled);
        AudioManagerScript.Instance.ChangeImage();
    }

    //ukljuci/isljuci zvuk SFX i promjena slike
    public void ToggleSfxAudio()
    {
        AudioManagerScript.Instance.ChangeImageSfx();
        AudioManagerScript.Instance.ToggleSfx(!AudioManagerScript.Instance.isSfxEnabled);
        
    }


}
