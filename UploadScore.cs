using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class UploadScore : MonoBehaviour
{
    //polje za unos Imena
    public TextMeshProUGUI playerNameInput;                                                                             

    //bodovi igraca
    private int playerScore;                                                                                            
    public TextMeshProUGUI scoreText;

    //dohvacanje spremljenih PlayerPrefs bodova i zbrajanje istih
    void Start()                                                                                                        
    {
        playerScore = 0;
        for (int i = 1; i <= SceneManager.sceneCountInBuildSettings; i++)
        {
            string scoreKey = "Score" + i;
            playerScore += PlayerPrefs.GetInt(scoreKey, 0);
            PlayerPrefs.DeleteKey(scoreKey);
        }
        scoreText.text = "Score: " + playerScore;


    }

    //Na klik gumba UPDATE pokrece se ova funkcija
    public void UpdateLeaderboard()                                                                                     
    {                                                                                                                               
        string playerName = playerNameInput.text;
        //Ako je igrac upisao ime izvodi se if
        if (!string.IsNullOrEmpty(playerName))
        {
            //provjerava se ako je postignuti score dovoljno velik kako bi stao na leaderboard
            bool isHighScore = Leaderboard.Instance.AddHighScore(playerScore, playerName);                              
            if (isHighScore)
            {
                //ako je score dovoljno velik azurira se leaderboard
                Leaderboard.Instance.UpdateLeaderboardUI();                                                             
            }

            //Pokreec se Leaderboard scene kako bi se igrac vidio na leaderbordu
            SceneManager.LoadScene("LeaderboardScene");
        }
        else
        {
            Debug.Log("Upisi ime");
        }
    }
}
