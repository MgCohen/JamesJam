using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [Header("Menus")]
    public GameObject creditsMenu;

    [Header("Buttons")]
    public Button startButton;
    public Button creditsButton;


    private void Awake()
    {
        SetButtons();
    }


    private void SetButtons()
    {
        UnityAction startAction = () => { StartGame(); };
        UnityAction creditsAction = () => { creditsMenu.SetActive(!creditsMenu.activeSelf); }; // ToDo: Call animation instead

        startButton.onClick.AddListener(startAction);
        creditsButton.onClick.AddListener(creditsAction);
    }


    private void StartGame()
    {
        FindObjectOfType<GameController>().lifes = 3;
        FindObjectOfType<GameController>().LoadRandomMinigame();
    }
}
