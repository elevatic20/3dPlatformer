using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Finish : MonoBehaviour
{
    //index trenutne scene
    int currentSceneIndex; 

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnTriggerEnter(Collider other)
    {
        //ako igrac koraci na platformu
        if (other.gameObject.tag == "Player")        
        {
            //DisplayUI funkcija za azuriranje bodova
            DisplayUI.Instance.UpdateFinishScore();

            //AduiManager skripta za pokretanje zvuka za kraj
            AudioManagerScript.Instance.PlayFinishSound();

            //poziva se funkcija koja pokrene drugu scenu
            Invoke(nameof(NextScene), 1.5f);      




        }
    }

    //funkcija za pokretanje sljedece scene
    private void NextScene()
    {
        if(currentSceneIndex == 3)
        {
            SceneManager.LoadScene("FinishMenu");
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        
        
    }
}