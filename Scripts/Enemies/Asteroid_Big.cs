using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Big : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDamage.instance.Damage(10);
        }
    }
}
