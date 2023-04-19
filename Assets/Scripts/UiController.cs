using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private GameObject InGame;
    [SerializeField]
    private GameObject EndGame;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private AudioClip[] gameplayMusic;
    [SerializeField]
    private AudioClip gameOverMusic;
    [SerializeField]
    private AudioClip previousClip;
    private AudioSource music;
    private int songId;

    private void Start()
    {
        music = SoundManager.Instance.GetMusicSource();
        songId = Random.Range(0, gameplayMusic.Length);
    }
    void Update()
    {
        ScreenVisibility(InGame,!player.isDead);
        ScreenVisibility(EndGame,player.isDead);
        MusicController();
    }

    private void MusicController()
    {
        music.clip = player.isDead ? gameOverMusic : gameplayMusic[songId];
        if (previousClip != music.clip)
        {
            music.Play();
        }

        previousClip = music.clip;
    }
    private void ScreenVisibility(GameObject screen, bool status)
    {
        screen.SetActive(status);
    }
}
