using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planets : MonoBehaviour
{
    private float _bottom = -15;

    [SerializeField]
    private float _speed = 1.5f;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Background.instance._speedMultiplier * Time.deltaTime);
        if (transform.position.y < _bottom)
        {
            Destroy(gameObject);
        }
    }
}
