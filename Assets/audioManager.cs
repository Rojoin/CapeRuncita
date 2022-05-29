using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
   
     private static audioManager instance;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = FindObjectOfType<AudioSource>();
        myAudio.PlayDelayed(10.0f);
    }
   public bool isPlayerDeath;
    private AudioSource[] allAudioSources;
public AudioSource myAudio;
public AudioClip playerDeath;
public AudioClip deathScreen;
public AudioClip itemPickUp;

    // Update is called once per frame
    void Update()
    {
        
    }
void awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
     private void OnEnable()
     {

     }
     private void OnDisable() 
     {
         
     }
     public void PlayerDies()
     {
         myAudio.PlayOneShot(playerDeath);
     }
     public void PlayerPickUp()
     {
        myAudio.PlayOneShot(itemPickUp);
     }
     public void PlayerDeath()
    {
        if (isPlayerDeath)
        {
            StopAllAudio();
            myAudio.PlayOneShot(playerDeath);
            isPlayerDeath = true;
            myAudio.PlayOneShot(deathScreen);
        }
    }
    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }
}
