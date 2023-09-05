using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    //instanciranje skripte
    public static Leaderboard Instance;

    //polje highscorova (njih 5)... text elementi koje povezujemo u UnityEditoru
    public TextMeshProUGUI[] highScoreTexts;                                                
    public int numberOfScores = 5;

    //lista u kojoj se spremaju vrijednosti highscorova (score i playerName)
    private List<HighScoreEntry> highScores = new List<HighScoreEntry>();                   

    //klasa u kojoj se nalaze varijable za BODOVE i IME igraca
    [System.Serializable]
    public class HighScoreEntry
    {
        public int score;
        public string playerName;
    }

    //instanciranje skripte
    private void Awake()                                                                    
    {
        Instance = this;
    }

    //pokrecu se funkcije koje ucitavaju highscorove i azuriraju leaderbord prema najboljim rezultatima
    void Start()                                                                            
    {
        LoadHighScores();
        UpdateLeaderboardUI();
    }

    void LoadHighScores()                                                                   
    {
        //Brisanje postojecih
        highScores.Clear();                                                                

        for (int i = 0; i < numberOfScores; i++)
        {
            string key = "HighScore" + i;
            if (PlayerPrefs.HasKey(key))
            {
                string json = PlayerPrefs.GetString(key);
                HighScoreEntry entry = JsonUtility.FromJson<HighScoreEntry>(json);
                //Ucitavanje highscoreva iz PlayerPrefs i dodavanje istih u listu
                highScores.Add(entry);                                                     
            }
            else
            {
                highScores.Add(new HighScoreEntry { score = 0, playerName = "-" });
            }
        }
        //Sortiranje leaderborda silazno

        highScores.Sort((a, b) => b.score.CompareTo(a.score));                                                  
    }

    //Azuriranje UI-a leaderborda s trenutnim najboljim rezultatima
    public void UpdateLeaderboardUI()                                                                           
    {
        for (int i = 0; i < numberOfScores; i++)
        {
            if (i < highScores.Count)
            {
                highScoreTexts[i].text = (i + 1) + ". " + highScores[i].playerName + ": " + highScores[i].score;
            }
            else
            {
                highScoreTexts[i].text = (i + 1) + ". -";
            }
        }
    }

    public bool AddHighScore(int score, string playerName)
    {
        bool isHighScore = false;
        HighScoreEntry newEntry = new HighScoreEntry { score = score, playerName = playerName };

        for (int i = 0; i < highScores.Count; i++)
        {
            if (score > highScores[i].score)
            {
                //Dodavanje novog highscore-a na odgovarajuce mjesto
                highScores.Insert(i, newEntry);

                //Uklanjanje najnizeg rezultata
                highScores.RemoveAt(highScores.Count - 1);                                          
                isHighScore = true;
                break;
            }
        }

        //ako je dodan novi u listu spremanje rezultata
        if (isHighScore)
        {
            SaveHighScores();                                                                       
        }
        //vracanje true vrijednosti ako je dodan novi highscore   
        return isHighScore;                                                                         
    }

    void SaveHighScores()
    {
        for (int i = 0; i < numberOfScores; i++)
        {
            string key = "HighScore" + i;
            if (i < highScores.Count)
            {
                string json = JsonUtility.ToJson(highScores[i]);
                //Spremanje highscoreva u playerprefs
                PlayerPrefs.SetString(key, json);                                                  
            }
            else
            {
                //ako nema rezultata za spremiti, obrise se rezultat s navedenim kljucem
                PlayerPrefs.DeleteKey(key);                                                        
            }
        }

        //spremanje promjena u playerPrefs
        PlayerPrefs.Save();                                                                        
    }
}