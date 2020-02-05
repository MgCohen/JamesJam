using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostGameScreen : MonoBehaviour
{

    [Header("Components")]
    public Animator anim;

    [Header("Texts")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text levelCompleteText;
    [SerializeField] private Text luckScoreText;

    [Header("Images")]
    [SerializeField] private Image[] lifesImage;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite deadSprite;

    [Header("Game Over")]
    public Text gameOverScoreText;
    public Text bestScoreText;

    [HideInInspector]
    public bool levelComplete;

    private int levelScore;
    private int luckScore;
    private string luckPhrase;


    private void Update()
    {
        scoreText.text = "You survived: " + HoursToDays(GameController.Instance.score);
        levelCompleteText.text = "Hours passed: " + levelScore.ToString();
        luckScoreText.text = luckPhrase + ":  " + luckScore.ToString();

        luckScoreText.gameObject.SetActive(GameController.Instance.useLuckScore);
    }

    private string HoursToDays(int hoursPassed)
    {
        int days = hoursPassed / 24;
        int hours = hoursPassed >= 24 ? (hoursPassed / 24) + hoursPassed : hoursPassed;
        string result = string.Format("{0}/{1}", days.ToString("d2"), hours.ToString("d2"));
        return result;
    }

    private void OnEnable()
    {
        if (GameController.Instance.useLifes)
        {
            EnableLifesImages(true);
            CheckLifesImages();
        }
        else EnableLifesImages(false);

        luckScoreText.gameObject.SetActive(GameController.Instance.useLuckScore);

        if (levelComplete)
        {
            levelScore = Random.Range(1, GameController.Instance.maxHoursGain);
            CheckWinLuck();
            StartCoroutine(ScoreUpdate());
        }
        else
        {
            levelScore = 0;
            CheckLooseLuck();

            foreach (var img in lifesImage)
                img.gameObject.SetActive(GameController.Instance.useLifes);
            if (GameController.Instance.useLifes)
                StartCoroutine(LooseLife());
            else
                GameController.Instance.lifes = 0;

            StartCoroutine(ScoreUpdate());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void EnableLifesImages(bool state)
    {
        foreach (var img in lifesImage)
            img.gameObject.SetActive(state);
    }

    private void CheckLifesImages()
    {
        for (int i = 0; i < lifesImage.Length; i++)
        {
            if (i > GameController.Instance.lifes - 1)
            {
                if (deadSprite != null)
                    lifesImage[i].sprite = deadSprite;
                lifesImage[i].CrossFadeAlpha(0.1f, 0f, true);
            }
        }
    }

    private IEnumerator LooseLife()
    {
        int lifes = GameController.Instance.lifes;
        if (deadSprite != null)
            lifesImage[lifes - 1].sprite = deadSprite;
        lifesImage[lifes - 1].CrossFadeAlpha(0.1f, 1f, true);
        yield return new WaitForSecondsRealtime(2f);
        GameController.Instance.DecreaseLifes();
        if (GameController.Instance.lifes <= 0)
            SetGameOver();
    }

    private IEnumerator ScoreUpdate()
    {
        yield return new WaitForSecondsRealtime(1f);
        // Level Completed
        while (levelScore > 0)
        {
            GameController.Instance.IncreaseScore(1);
            GameController.Instance.sfxSource.PlayOneShot(GameController.Instance.scoreSound);
            levelScore--;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.5f);

        // Luck Score
        if (GameController.Instance.useLuckScore)
        {
            if (luckScore > 0)
            {
                while (luckScore > 0)
                {
                    GameController.Instance.IncreaseScore(1);
                    GameController.Instance.sfxSource.PlayOneShot(GameController.Instance.scoreSound);
                    luckScore--;
                    yield return new WaitForSecondsRealtime(0.01f);
                }
            }
            else
            {
                while (luckScore < 0)
                {
                    GameController.Instance.IncreaseScore(-1);
                    luckScore++;
                    yield return new WaitForSecondsRealtime(0.01f);
                }
            }
        }

        yield return new WaitForSecondsRealtime(1f);
        if (GameController.Instance.lifes > 0)
            GameController.Instance.LoadRandomMinigame();
        else SetGameOver();
    }

    private void CheckWinLuck()
    {
        string[] bad = { "Bad luck", "Could be better" };
        string[] good = { "For style", "Nice weather today", "Because you're special", "Because you deserve it", "Yay!", ":-) " };
        if(Random.Range(0, bad.Length + good.Length) > bad.Length)
        {
            int index = Random.Range(0, good.Length);
            int randomScore = Random.Range(10, 60);
            luckPhrase = good[index];
            luckScore = randomScore;
        }
        else
        {
            int index = Random.Range(0, bad.Length);
            int randomScore = Random.Range(-40, -10);
            luckPhrase = bad[index];
            luckScore = randomScore;
        }
    }

    private void CheckLooseLuck()
    {
        string[] bad = { "Bad luck", "Outch!", "Mind your language", "Boo!" };
        string[] good = { "For style", "Nice weather today", "Because you're special", "For trying", ":-) " };
        if (Random.Range(0, bad.Length + good.Length) > bad.Length)
        {
            int index = Random.Range(0, good.Length);
            int randomScore = Random.Range(10, 60);
            luckPhrase = good[index];
            luckScore = randomScore;
        }
        else
        {
            int index = Random.Range(0, bad.Length);
            int randomScore = Random.Range(-40, -10);
            luckPhrase = bad[index];
            luckScore = randomScore;
        }
    }

    private void SetGameOver()
    {
        anim.SetBool("GameOver", true);
        GameController.Instance.GameOver();
        if (defaultSprite != null)
        {
            for (int i = 0; i < lifesImage.Length; i++)
                lifesImage[i].sprite = defaultSprite;
        }
    }
}
