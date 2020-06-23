using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;

    public static Shake instance;

    private void Awake()
    {
        instance = this;
    }

    public void Shake1()
    {
        _anim.SetTrigger("shake1");
    }

    public void Shake2()
    {
        _anim.SetTrigger("shake2");
    }

}
