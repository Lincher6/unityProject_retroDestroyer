using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private int _rotator = 200;
    private float _offset = -1.5f;
    private float _speed = 4;

    void Start()
    {
        StartCoroutine(Movement());
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * _rotator * Time.deltaTime);
        transform.Translate(new Vector3(_offset, -1, 0) * _speed * Time.deltaTime, Space.World);
    }

    IEnumerator Movement()
    {
        while(true)
        {
            while (_offset >= -1.5f)
            {
                _offset -= 0.5f;
                yield return new WaitForSeconds(0.1f);
            }

            while (_offset <= 1.5)
            {
                _offset += 0.5f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
