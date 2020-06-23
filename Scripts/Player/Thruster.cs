using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    private Vector3 _originalSize;
    public static Thruster instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _originalSize = transform.localScale;
    }

    public void ScaleUp()
    {
        transform.localScale *= 1.2f;
    }

    public void ScaleDown()
    {
        transform.localScale =_originalSize;
    }
}
