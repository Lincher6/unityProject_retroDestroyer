using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 15;
    [SerializeField]
    private float _angle = 0;
    [SerializeField]
    private GameObject _laserDamage;

    private Vector3 _position;

    void Start()
    {
        _position = new Vector3(1 * _angle, 1 * _speed);
    }

    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        transform.Translate(_position * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Instantiate(_laserDamage, transform.position, Quaternion.identity);
        }
    }
}
