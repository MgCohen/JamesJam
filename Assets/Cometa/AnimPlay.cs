using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlay : MonoBehaviour
{
    public Animator anim;
    public float delay;
    public float repeatDelay;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        Invoke("Do", delay);
    }

    private void Do()
    {
        anim.Play("Translate");
        Invoke("Do", repeatDelay);


    }
}
