using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using System.Globalization;


public class PlayerLife : MonoBehaviour
{

    public Renderer playerRenderer;                     //renderer igraca
    private float flashCounter;                         //brojac treptaja
    private bool playerHit;                             //ako je igrac dotaknut == TRUE
    public float flashLenght;                           //duljina treptaja

    private CheckpointManager checkpointManager;        //dohvacenje skripte CheckpontManager
    private CharacterController characterController;    //dohvacenje komponente igraca CharacterController

    public int lives = 3;                               //broj zivota igraca
    private int currentLives;
    private int updateLives;
    [SerializeField] TextMeshProUGUI livesText;         //display zivota na zaslonu

    private void Start()
    {
        //postavljanje broja zivota i prikazivanje istog na zaslonu
        currentLives = lives;
        updateLives = PlayerPrefs.GetInt("Lives");
        
        if (updateLives < 3 && updateLives > 0)
        {
            currentLives = updateLives;
            UpdateLives();
        }

        UpdateLives();

        //dohvacanje zadanih komponenti
        characterController = GetComponent<CharacterController>();
        checkpointManager = GetComponent<CheckpointManager>();
    }

    //ako se dogodi kolizija
    private void OnTriggerEnter(Collider other)        
    {
        //ako je u koliziji neprijatelj
        if (other.gameObject.tag == "EnemyTag")         
        {
            //izvodenje zvuka za udarac od neprijatelja
            AudioManagerScript.Instance.PlayHitSound();

            playerHit = true;
            playerRenderer.enabled = false;
            flashCounter = flashLenght;

            //onemogucavanje kretanja
            characterController.enabled = false;

            //onemugocavanje animacije
            GetComponent<Animator>().enabled = false;

            //onemugocavanje sripte kretanja
            GetComponent<PlayerMovement>().enabled = false;


            //logika provjere zivota igraca, te naknadnog postupanja u odnosu na br zivota
            if (currentLives > 1)                                  
            {
                Invoke(nameof(Respawn), 0.7f);
            }
            else
            {
                Invoke(nameof(GameOver), 0.7f);
            }
        }


        //ako je igrac pao s platforme
        if (other.gameObject.tag == "PlayerFall")          
        {
            //izvodjenje zvuka za pad s platforme
            AudioManagerScript.Instance.PlayFallSound();
            if(currentLives > 1)
            {
                Invoke(nameof(Respawn), 1.1f);
            }
            else
            {
                Invoke(nameof(GameOver), 1.1f);
            }

            

        }

    }
    void Update(){
        //treptanje igraca ako ga je neprijatelj dotaknuo
        flashCounter -= Time.deltaTime;
        if(playerHit == true){
            if(flashCounter <= 0){
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLenght;
            }
        }
    }

    //pozivanje scene za kraj igre i postavljanje "tvornickih" postavki
    void GameOver()                                                     
    {
        currentLives = currentLives - 1;
        UpdateLives();
        DisplayUI.Instance.UpdateFinishScore();
        
        
        SceneManager.LoadScene("GameOver");
        playerHit = false;
        currentLives = lives;
        UpdateLives();
    }

    //ukoliko igrac ima vise od 0 zivota
    void Respawn()
    {
        //smanjuje se broj zivota
        currentLives = currentLives - 1;

        //update zivota
        UpdateLives();

        //dohvacanje zadnjeg checkpointa
        Transform respawnPoint = checkpointManager.GetLastCheckpoint(); 
        

        if (respawnPoint != null)
        {
            characterController.enabled = false;                        //onemogucavanje komponente charactercontroller
            transform.position = respawnPoint.position;                 //postavljanje igraca na mjesto zadnjeg Checkponta
            characterController.enabled = true;                         //omogucavanje komponente charactercontroller
            GetComponent<Animator>().enabled = true;                    //omogucavanje animacija
            GetComponent<PlayerMovement>().enabled = true;              //omogucavanje skripte za kretanje
            playerHit = false;                                          //postavljanje pocetne postavke varijable "playerHit" kako igrac ne bi vise treptao
            playerRenderer.enabled = true;                              //omogucavanje playerRenderera
        }
    }

    //funkcija za ispis broja zivota na zalon i spremanje istih u PlayerPrefs(trenutnu memoriju)
    void UpdateLives()                                                  
    {
        livesText.text = "" + currentLives;
        PlayerPrefs.SetInt("Lives", currentLives);
        PlayerPrefs.Save();
    }

}
