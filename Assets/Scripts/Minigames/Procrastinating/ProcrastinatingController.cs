using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcrastinatingController : Minigame
{

    public Slider slider;
    [Range(0f, 1f)]
    public float valueToReach;


    public override void UpdateBehaviour(float timer)
    {
        success = slider.value >= valueToReach;
        base.UpdateBehaviour(timer);
    }

}
