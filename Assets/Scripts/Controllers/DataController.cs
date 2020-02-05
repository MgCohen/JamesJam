using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{

    public static int bestScore = 0;

    void Start()
    {
        //PlayerPrefs.DeleteAll ();

        LoadPlayerProgress();
    }

    public void SubmitNewPlayerScore(int newScore)
    {
        if (newScore > bestScore)
        {
            bestScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GetHighestPlayerScore()
    {
        return bestScore;
    }

    void LoadPlayerProgress()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }
    }

    void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
    }

}

