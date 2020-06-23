using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy3sec : MonoBehaviour
{
    [SerializeField]
    private float _time = 3;

    void Start()
    {
        Destroy(gameObject, _time);
    }
}
