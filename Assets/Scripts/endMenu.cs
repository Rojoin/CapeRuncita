using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endMenu : MonoBehaviour
{
    [SerializeField] GameObject endMenuUI;
    [SerializeField] PlayerController jugador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(jugador.isDead)
        {
                StartCoroutine("WaitforMenu");
        }
    }

    public void Retry()
    {
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(1);
    }
    public void LoadMenu()
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
           SceneManager.UnloadSceneAsync(1);
        }
        public void Quitgame()
        {
            Debug.Log("Quit!");
          Application.Quit();
        }
 IEnumerator WaitforMenu()
 {
    yield return new WaitForSeconds(1);
    endMenuUI.SetActive(true);
 }
}
