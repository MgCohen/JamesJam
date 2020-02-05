using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameController : MonoBehaviour
{

    [Header("Minigame")]
    public Minigame minigame;

    [Header("Settings")]
    public float timer;

    private float defaultTimer;

    [Header("HUD")]
    [SerializeField] private Image timerBar;

    [Header("Quick tutorial")]
    [SerializeField] private Text title;
    [SerializeField] private Text tutorial;
    [Space]
    [SerializeField] private string minigameTitle;
    [TextArea(0, 3)]
    [SerializeField] private string tutorialMessage;


    private void Start()
    {
        defaultTimer = timer;
        StartCoroutine(QuickTutorial());
        GameController.Instance.currentMinigame = this;
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            timer -= Time.deltaTime;
            CheckProgress();
            TimerBarUpdate();
        }
    }

    private void TimerBarUpdate()
    {
        timerBar.fillAmount = SetTimer(timer, 0, defaultTimer, 0, 1);
    }

    private float SetTimer(float time, float inMin, float inMax, float outMin, float outMax)
    {
        return (time - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }


    private IEnumerator QuickTutorial()
    {
        title.text = minigameTitle;
        tutorial.text = tutorialMessage;
        yield return new WaitForSecondsRealtime(1f);
        title.CrossFadeAlpha(0f, 2f, true);
        tutorial.CrossFadeAlpha(0f, 2f, true);
    }

    public void CheckProgress()
    {
        minigame.UpdateBehaviour(timer);
    }

}
