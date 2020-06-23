using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int _hp = 2;
    [SerializeField]
    private int _damageToPlayer = 1;
    [SerializeField]
    private int _scoreOnDestroy = 15;

    private float _screenBound;

    [SerializeField]
    private GameObject _explosionPrefab;

    void Start()
    {
        _screenBound = -7;
    }

    void Update()
    {
        if (transform.position.y < _screenBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_Projectile")
        {
            Destroy(collision.gameObject);
            _hp--;

        }

        else if (collision.tag == "Player")
        {
            PlayerDamage.instance.Damage(_damageToPlayer);
            _hp -= 10;
        }

        if (_hp <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        UIManager.instance.UpdateScore(_scoreOnDestroy);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
    }
}
