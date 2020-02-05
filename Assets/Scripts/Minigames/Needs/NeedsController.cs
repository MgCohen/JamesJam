using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NeedsController : Minigame
{

    public GameObject variant01;
    public GameObject variant02;

    public List<Button> flushButtons;
    public List<Button> destructionButtons;


    private void Start()
    {
        bool variant = Random.Range(0f, 2f) > 0 ? true : false;
        variant01.SetActive(variant);
        variant02.SetActive(!variant);

        SetButtons();
    }


    public override void UpdateBehaviour(float timer)
    {
        base.UpdateBehaviour(timer);
    }


    private void SetButtons()
    {
        UnityAction flush = () => { GameController.Instance.WinMinigame(); };
        UnityAction destruction = () => { GameController.Instance.LooseMinigame(); };

        foreach (var b in flushButtons)
            b.onClick.AddListener(flush);

        foreach (var b in destructionButtons)
            b.onClick.AddListener(destruction);
    }

}
