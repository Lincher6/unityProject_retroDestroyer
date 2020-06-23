using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSmall : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2;
    [SerializeField]
    private float _rotationAngle = 0;
    // 0 - random rotation

    void Start()
    {
        if (_rotationAngle == 0)
        {
            _rotationAngle = Random.Range(-100, 100);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0, 0, _rotationAngle * Time.deltaTime));
    }
}
