using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularSpaceship : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;
    [SerializeField]
    private float _hp = 1;
    [SerializeField]
    private int _scoreOnDestroy = 10;

    private bool _isAlive = true;

    private float _screenBound = -7;
    private Animator _anim;

    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

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
            PlayerDamage.instance.Damage(1);
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
        _anim.SetTrigger("OnEnemyDeath");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<AudioSource>().Play();
        _isAlive = false;
        Destroy(gameObject, 2.5f);
    }

    public bool isAive()
    {
        return _isAlive;
    }
}
