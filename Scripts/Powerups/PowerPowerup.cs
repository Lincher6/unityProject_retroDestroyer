﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPowerup : MonoBehaviour
{
    private float _speed;

    [SerializeField]
    private GameObject _powerupSound;

    void Start()
    {
        _speed = 3;
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerFire.instance.PowerIncrease();

            Instantiate(_powerupSound, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
