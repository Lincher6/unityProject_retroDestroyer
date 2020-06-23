using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;
    [SerializeField]
    private int _playerDamage = 1;
    [SerializeField]
    private GameObject _damagePrefab;

    public Vector3 vector;

    void Start()
    {
        vector = new Vector3(0, -1, 0);
    }

    void Update()
    {
        transform.Translate(vector * _speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Instantiate(_damagePrefab, transform.position, Quaternion.identity);
            PlayerDamage.instance.Damage(_playerDamage);
            Destroy(gameObject);
        }
    }
}
