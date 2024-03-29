using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] UIScore scoreValue;
    [SerializeField] private AudioClip mainMusic;
    void Start()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        uint finalScore = scoreValue.GetScore();
        uint highestScore = (uint)PlayerPrefs.GetFloat("HighScore");

        if (finalScore > highestScore)
        {
            highestScore = finalScore;
            PlayerPrefs.SetFloat("HighScore", (float)finalScore);
        }

        score.text = "Score: " + finalScore;
        highScore.text = "HighScore: " + highestScore;
    }

#if UNITY_ANDROID
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
    }
#endif

    public void Retry()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SoundManager.Instance.GetMusicSource().clip = mainMusic;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}