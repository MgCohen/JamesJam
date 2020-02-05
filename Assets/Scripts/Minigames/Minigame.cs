using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{

    public bool success { get; protected set; }


    public virtual void UpdateBehaviour(float timer) 
    {
        if (timer <= 0)
        {
            if (success)
                GameController.Instance.WinMinigame();
            else
                GameController.Instance.LooseMinigame();
        }
    }

}
