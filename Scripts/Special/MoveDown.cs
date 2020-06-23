using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}
