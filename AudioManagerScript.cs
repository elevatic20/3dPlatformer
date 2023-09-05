using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManagerScript : MonoBehaviour
{
    //Instanciranje skripte
    public static AudioManagerScript Instance;

    //Polje u koje se sprema pozadinska glazba
    [Header("Background Music")]
    public AudioClip[] backgroundMusic;                     
    private AudioSource backgroundMusicSource;

    //index trenutne scene u Build Settings
    private int currentSceneIndex = -1;

    //SFX audio
    [Header("Sound Effects")]                               
    public AudioClip jumpSound;
    public AudioClip collectSound;
    public AudioClip fallSound;
    public AudioClip hitSound;
    public AudioClip finishSound;
    public AudioClip checkpointSound;
    private AudioSource sfxSource;

    //Globalni enable/disable audio za: Pozadinsku glazbu i SFX zvukove                                                   
    public bool isBackgroundMusicEnabled = true;            
    public bool isSfxEnabled = true;

    //BG button i slike
    public Button tmpButton;                                
    public Sprite newImage;
    public Sprite oldImage;

    //SFX button i slike
    public Button tmpButtonSfx;                             
    public Sprite newImageSfx;
    public Sprite oldImageSfx;

    //instanciranje AudioManager gameObjekta
    private void Awake()                                    
    {
        if (Instance == null)
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Inicijaliziranje audio izvora prema audio klipovima
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();         
        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    //Pokretanje funkcije PlayBackgroundMusic(s indekom 0) i pokretanje funkcije OnSceneLoaded kad se scena ucita
    private void Start()                                                        
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;                              
        PlayBackgroundMusic(0);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        //Provjerava je li se scena promijenila
        if (scene.buildIndex != currentSceneIndex || currentSceneIndex == -1)
        {
            //Debug.Log("Tu sam");
            currentSceneIndex = scene.buildIndex;

            //Pokrece pozadinsku glazbu ako je dozvoljena
            if (isBackgroundMusicEnabled)
            {
                //Index se mijenja prema trenutnoj sceni
                PlayBackgroundMusic(currentSceneIndex); 
                
            }
            else
            {
                StopBackgroundMusic();
            }
        }
    }

    public void ToggleBackgroundMusic(bool play)
    {
        isBackgroundMusicEnabled = play;

        //ako je iskljuceno, zaustavlja BG music
        if (!play)
        {
            StopBackgroundMusic();
        }
        //Akoje je ukljuceno, pokrece BG music prema trenutnoj sceni
        else
        {
            PlayBackgroundMusic(currentSceneIndex);
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }
    }

    //iskljucivanje SFX zvukova
    public void ToggleSfx(bool play)
    {
        isSfxEnabled = play;
    }


    //javna funkcija za izvodenja SFX zvuka: Skok
    public void PlayJumpSound()
    {

        if(isSfxEnabled)
        {
            sfxSource.volume = 0.2f;
            sfxSource.pitch = 0.7f;
            sfxSource.PlayOneShot(jumpSound);
        }
        
    }

    //javna funkcija za izvodenja SFX zvuka: Checkpoint
    public void PlayCheckpointSound()
    {

        if(isSfxEnabled)
        {
            sfxSource.volume = 0.2f;
            sfxSource.pitch = 0.7f;
            sfxSource.PlayOneShot(checkpointSound);
        }
        
    }

    //javna funkcija za izvodenja SFX zvuka: Finish
    public void PlayFinishSound()
    {
        if (isSfxEnabled)
        {
            sfxSource.volume = 0.2f;
            sfxSource.pitch = 0.7f;
            sfxSource.PlayOneShot(finishSound);
        }
        
    }

    //javna funkcija za izvodenja SFX zvuka: Collect
    public void PlayCollectSound()
    {
        if (isSfxEnabled)
        {
            sfxSource.volume = 0.2f;
            sfxSource.pitch = 0.6f;
            sfxSource.PlayOneShot(collectSound);
        }
        
    }

    //javna funkcija za izvodenja SFX zvuka: Fall
    public void PlayFallSound()
    {
        if (isSfxEnabled)
        {
            sfxSource.volume = 0.2f;
            sfxSource.pitch = 0.7f;
            sfxSource.PlayOneShot(fallSound);
        }
       
    }

    //javna funkcija za izvodenja SFX zvuka: Hit
    public void PlayHitSound()
    {
        if (isSfxEnabled)
        {
            sfxSource.volume = 0.2f;
            sfxSource.pitch = 1.5f;
            sfxSource.PlayOneShot(hitSound);
        }
        
    }

    //pustanje BG music
    public void PlayBackgroundMusic(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < backgroundMusic.Length)
        {
            //Debug.Log(trackIndex);
            backgroundMusicSource.Stop();
            backgroundMusicSource.clip = backgroundMusic[trackIndex];
            backgroundMusicSource.volume = 0.21f;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    //zaustavljanje BG music
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    //promjena slike gumba BG music
    public void ChangeImage()
    {
        Image buttonImage = tmpButton.GetComponentInChildren<Image>();
        if (isBackgroundMusicEnabled)
        {
            buttonImage.sprite = oldImage;
        }
        else
        {
            buttonImage.sprite = newImage;
        }

    }

    //Promjena slike gumba SFX zvukova
    public void ChangeImageSfx()
    {
        Image buttonImageSfx = tmpButtonSfx.GetComponentInChildren<Image>();
        if (!isSfxEnabled)
        {
            buttonImageSfx.sprite = oldImageSfx;
        }
        else
        {
            buttonImageSfx.sprite = newImageSfx;
        }

    }


}

