using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    private float _bottom = -15;
    private float _top = 20;
    private Vector3 _position;

    [SerializeField]
    private float _speed = 1.5f;
    [SerializeField]
    private GameObject _starsPrefab;

    void Start()
    {
        _position = new Vector3(transform.position.x, _top, transform.position.z);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Background.instance._speedMultiplier * Time.deltaTime);

        if (transform.position.y < _bottom)
        {
            Instantiate(_starsPrefab, _position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
