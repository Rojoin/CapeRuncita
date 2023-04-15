using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] SceneController scenes;
    
     /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
            pauseMenuUI.SetActive(false);
            GameIsPaused = false;
            Time.timeScale = 1f;
         //   scenes.timerActive = true;
    }
    public void Pause()
        {
          //  scenes.timerActive = false;
            pauseMenuUI.SetActive(true);
            GameIsPaused = true;
            Time.timeScale = 0f;
        }
        public void LoadMenu()
        {
            afterResume();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
            SceneManager.UnloadSceneAsync(1);
        }
        public void Quitgame()
        {
            Debug.Log("Quit!");
          Application.Quit();
        }
        public void Retry()
    {
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(1);
         afterResume();
        
    }
    public void afterResume()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
    }
    public void PauseMenuButton()
    {
    if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
    }
}
