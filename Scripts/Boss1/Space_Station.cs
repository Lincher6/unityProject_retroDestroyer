using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space_Station : MonoBehaviour
{
    private Animator _anim;

    void Start()
    {
        _anim = transform.GetComponent<Animator>();
    }

    void Update()
    {
        if (transform.position.y < -1)
        {
            _anim.SetTrigger("Appear");
        }
    }
}
