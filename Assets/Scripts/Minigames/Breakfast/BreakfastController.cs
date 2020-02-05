using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakfastController : Minigame
{

    public Button shieldButton;
    public Image shieldBar;
    public Image shieldImg;
    public float aincrease;
    [Range(0f, 1f)]
    public float valueToReach;


    private void Start()
    {
        shieldImg.color = new Color(0, 0, 0, 0);
        UnityEngine.Events.UnityAction action = () => { ChargeShield(); };
        shieldButton.onClick.AddListener(action);
    }


    public override void UpdateBehaviour(float timer)
    {
        success = shieldBar.fillAmount >= valueToReach;
        base.UpdateBehaviour(timer);
    }


    private void ChargeShield()
    {
        shieldBar.fillAmount += Random.Range(0.01f, 0.15f);
        var color = shieldImg.color;
        color.a += aincrease;
        shieldImg.color = color;
    }
}
