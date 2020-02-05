using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LightController : Minigame
{
    public Button lightButton;
    public Button destructionButton;


    private void Start()
    {
        SetButtons();
    }


    public override void UpdateBehaviour(float timer)
    {
        base.UpdateBehaviour(timer);
    }


    private void SetButtons()
    {
        UnityAction lightup = () => { GameController.Instance.WinMinigame(); };
        UnityAction destruction = () => { GameController.Instance.LooseMinigame(); };

        lightButton.onClick.AddListener(lightup);
        destructionButton.onClick.AddListener(destruction);
    }

}
