using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    public int score { get; private set; }

    [Header("Audios")]
    [SerializeField] private AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;
    public AudioClip scoreSound;

    [Header("Settings")]
    public int maxHoursGain;
    public int lifes;
    public bool useLifes;
    public bool useLuckScore;

    [Header("Screens")]
    [SerializeField] private PostGameScreen postGameScreen;

    [HideInInspector]
    public MinigameController currentMinigame;

    private int minigameIndex;

    //DontDestroyOnLoad
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        minigameIndex = 0;
    }

    private void Update()
    {
        //if (SceneManager.GetActiveScene().buildIndex == 0)
        //{
        //    Time.timeScale = 1f;
        //    musicSource.pitch = 1f;
        //    postGameScreen.gameObject.SetActive(false);
        //}
        //else
        //    musicSource.pitch = 1.5f;
    }

    public void LoadRandomMinigame()
    {
        if (musicSource.clip != gameplayMusic)
        {
            musicSource.clip = gameplayMusic;
            musicSource.Play();
        }

        postGameScreen.anim.SetBool("GameOver", false);

        int randomIndex = Random.Range(1, SceneManager.sceneCountInBuildSettings);
        while(randomIndex == minigameIndex)
            randomIndex = Random.Range(1, SceneManager.sceneCountInBuildSettings);
        minigameIndex = randomIndex;

        postGameScreen.gameObject.SetActive(false);
        SceneManager.LoadScene(minigameIndex);
        Time.timeScale = 1f;
    }

    public void WinMinigame()
    {
        Debug.Log("WIN");
        // currentMinigame.ResultAnim();
        Time.timeScale = 0;
        postGameScreen.levelComplete = true;
        postGameScreen.gameObject.SetActive(true);
    }

    public void LooseMinigame()
    {
        Debug.Log("LOOSE");
        // currentMinigame.ResultAnim();
        Time.timeScale = 0;
        postGameScreen.levelComplete = false;
        postGameScreen.gameObject.SetActive(true);
    }

    public void IncreaseScore(int multiplyer)
    {
        score += 1 * multiplyer;
    }

    public void DecreaseLifes()
    {
        lifes--;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        FindObjectOfType<DataController>().SubmitNewPlayerScore(score);
        postGameScreen.gameOverScoreText.text = score + "!";
        postGameScreen.bestScoreText.text = "Your longest life had: " + DataController.bestScore;
        if (score == DataController.bestScore)
            postGameScreen.bestScoreText.text = "That was your longest life!";

        score = 0;
        lifes = 0;
    }

    public void BackToMainMenu()
    {
        lifes = 3;
        musicSource.clip = menuMusic;
        musicSource.Play();
        postGameScreen.anim.SetBool("GameOver", false);
        postGameScreen.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }

    // GameOverScreen
    // GameOver();
    // Level -count how much minigames has been loaded-
    // Dificult -based on level-

}
