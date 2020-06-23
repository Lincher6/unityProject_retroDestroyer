using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nebula : MonoBehaviour
{
    private float _bottom = -40.9f;
    private float _top = 40.8f;
    private Vector3 _position;

    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private GameObject _nebulaPrefab;

    void Start()
    {
        _position = new Vector3(transform.position.x, _top, transform.position.z);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Background.instance._speedMultiplier * Time.deltaTime);

        if (transform.position.y < _bottom)
        {
            Instantiate(_nebulaPrefab, _position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
