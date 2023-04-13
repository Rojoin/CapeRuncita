using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public bool creditsOn = false;
    [SerializeField] GameObject screenMenu;
    [SerializeField] GameObject screenCredits;

    void Update()
    {
        ScreenVisibility(screenMenu, !creditsOn);
        ScreenVisibility(screenCredits, creditsOn);
    }
    public void PlayGame()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
   
    public void ExitGame()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        Debug.Log("Quit!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
   	    Application.Quit();
#endif
    }
    IEnumerator TiempoJugar()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //soundManager.SelecionarAudio(0,0.5f);
    }
    public void OpenMoreGamesUrl()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        Application.OpenURL("https://imagecampus.itch.io/");
    }
    private void ScreenVisibility(GameObject screen, bool status)
    {
        screen.SetActive(status);
    }
    public void SetCredits()
    {
        creditsOn = !creditsOn;
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
    }
}
