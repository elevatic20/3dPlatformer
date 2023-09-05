using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class DisplayUI : MonoBehaviour
{
    //instanciranje skripte
    public static DisplayUI Instance;

    //text polja za unos sira, timer i bodove
    public TextMeshProUGUI cheeseCount;                 
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    //varijable potrebne za timer
    public float timeLeft;                              
    public bool timerOn = false;
    private float minute;
    private float sec;

    //varijable potrebne za izracunavanje bodova
    public float remainTime;                            
    private float score = 0;
    public int valueOfCheese;
    public int valueOfSec;
    int roundedValue;
    public int livesValue = 6;

    //varijabla za spremanje build indeksa trenutne scene
    int currentSceneIndex;

    //instanciranje skripte
    void Awake()                                       
    {
        Instance = this;
    }


    //pokretanje timera
    void Start()                                                                        
    {
        scoreText.text = "Score: " + score.ToString();
        timerOn = true;
    }

    //dohvaca broj sireva u Inventory skripti i povecava score za rijednost sira(100)
    public void UpdateCheeseCount(Inventory playerInventory)                            
    {
        cheeseCount.text = playerInventory.NumberOfCheese.ToString();
        score += valueOfCheese;
        scoreText.text = "Score: " + score.ToString();
    }

    //logika timera
    void Update()                                                                       
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                //Debug.Log("Z timer ");
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    //pretvaranje broj sekundi u minute i sekunde prema formatu
    void UpdateTimer(float currentTime)                                                 
    {
        currentTime += 1;
        minute = Mathf.FloorToInt(currentTime / 60);
        sec = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minute, sec);
        remainTime = minute * 60 + sec;
    }

    //azuriranje finalnih bodova prema dolje navedenoj logici
    public void UpdateFinishScore()                                                 
    {
        int lives = PlayerPrefs.GetInt("Lives");
        //Debug.Log("Broj zivota: " + lives);

        if (lives == 0) {
            //ne uzimamo u obzir preostalo vrijeme i ne dodajemo bodove za preostale živote                                                                      
            roundedValue = Mathf.RoundToInt(score);

        }
        else if (lives > 0)                                                     
        {
            //uzimamo u obzir preostalo vrijeme i dodajemo bodove za preostale živote
            score += (timeLeft * valueOfSec) * (lives*livesValue);
            roundedValue = Mathf.RoundToInt(score);
        }
        //Debug.Log("TimeLeft " + timeLeft);
        scoreText.text = "Score: " + roundedValue.ToString();

        SaveLoadData();
        //Debug.Log("Broj zivota2: " + lives);
        timerOn = false;
    }

    //spremanje bodova u PlayerPrefs za svaki level koristeci buildindex scene
    public void SaveLoadData()                                              
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        PlayerPrefs.SetInt("Score" + currentSceneIndex, roundedValue);
        Debug.Log("Score" + currentSceneIndex + ": " + roundedValue);
        PlayerPrefs.Save();
    }

}