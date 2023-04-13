using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    SoundManager soundManager;
    // Start is called before the first frame update
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    public void PlayGame()
    {
        StartCoroutine("TiempoJugar");
    }
    public void soundUI()
    {
        soundManager.SelecionarAudio(0, 0.5f);
    }
    public void ExitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    IEnumerator TiempoJugar()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //soundManager.SelecionarAudio(0,0.5f);
    }
    public void OpenMoreGamesUrl()
    {
        Application.OpenURL("https://imagecampus.itch.io/");
    }
}
