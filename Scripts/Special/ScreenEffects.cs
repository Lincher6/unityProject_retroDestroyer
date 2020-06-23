using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffects : MonoBehaviour
{
    private Animator _anim;

    public static ScreenEffects instance;

    private void Awake()
    {
        instance = this;
        _anim = transform.GetComponent<Animator>();
    }

    public void Flash()
    {
        _anim.SetTrigger("Flash");
    }

    public void Fade()
    {
        _anim.SetTrigger("Fade");
    }

    public void Appear()
    {
        _anim.SetTrigger("Appear");
    }

    public void RedFlash()
    {
        _anim.SetTrigger("Red_Flash");
    }
}
