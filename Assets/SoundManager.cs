using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{ public static SoundManager Instance;
    public bool isAudioOn = true;
    [SerializeField] AudioClip[] fuenteAudio;

    private  AudioSource controlador;
    

    // Start is called before the first frame update
    private void Awake() 
    { if(Instance == null)
            {
                Instance= this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        
        controlador = GetComponent<AudioSource>();
        
    }
    void Update()
    {
        cambiarAudio();
    }
    public void SelecionarAudio(int indice, float volumen)
    {
        controlador.PlayOneShot(fuenteAudio[indice],volumen);
    }
    public void apagarAudio()
    {
        isAudioOn = false;
    }
    public void cambiarAudio()
    {
        // if(isAudioOn)
        // {
        //     isAudioOn = false;
        //     controlador.mute = true;
        // }
        // else if(!isAudioOn)
        // {
        //     isAudioOn = true;
        //     controlador.mute = false;
        //     Debug.Log("Desmuteado");
        // }
        if(isAudioOn)
        {
            AudioListener.volume = 1;
           // controlador.mute = false;
        }
        else{
            AudioListener.volume = 0;
           // controlador.mute = true;
        }
    }
     public void changeBoolAudio()
     {
         if(isAudioOn)
        {
            isAudioOn = false;
        }
        else{
            isAudioOn = true;
            
        }
     }
    IEnumerator audioON()
    {
        yield return new WaitForSeconds(0.1f);

    }
    IEnumerator audioOFF()
    {
        yield return new WaitForSeconds(0.1f);
        isAudioOn = true;
            controlador.mute = false;

    }
}
