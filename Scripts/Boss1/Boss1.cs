using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [SerializeField]
    private float _hp = 200;
    [SerializeField]
    private int _scoreOnDestroy = 500;
    private float _hpMax;
    [SerializeField]
    private float _speed = 2;
    private bool isSecondForm = false;
    private bool isLeft = false;
    private bool isDown = true;
    private bool isRight = false;
    [SerializeField]
    private GameObject _explosionPrefab;

    private Animator _anim;
    private AudioSource _audio;

    public static Boss1 instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        MoveDown();
        MoveLeft();
        MoveRight();

    }

    void Start()
    {

        _hpMax = _hp;
        _anim = transform.GetComponent<Animator>();
        _audio = transform.GetComponent<AudioSource>();

        //StartCoroutine(Appear());
    }

    void MoveDown()
    {
        if (transform.position.y > 2.5f && isDown)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        else if (isDown)
        {
            isDown = false;
            isLeft = true;
        }
    }

    void MoveLeft()
    {
        if (transform.position.x > -6 && isLeft)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        else if (isLeft)
        {
            isLeft = false;
            isRight = true;
        }
    }

    void MoveRight()
    {
        if (transform.position.x < 6 && isRight)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        else if (isRight)
        {
            isLeft = true;
            isRight = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_Projectile"))
        {
            _hp--;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            PlayerDamage.instance.Damage(10);
        }

        if ((_hp < _hpMax / 2) && !isSecondForm)
        {
            isSecondForm = true;
            SecondForm();
            StartCoroutine(SecondFormRoutine());
        }

        if (_hp < 1)
        {
            transform.GetComponent<BoxCollider2D>().enabled = false;
            Death();
        }
    }

    void Death()
    {
        UIManager.instance.UpdateScore(_scoreOnDestroy);
        SpawnManager.instance.BossKilled();

        StartCoroutine(DeathRoutine());

        Destroy(gameObject, 1.2f);
    }

    IEnumerator DeathRoutine()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(_explosionPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(_explosionPrefab, transform.position + Vector3.left * 2, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(_explosionPrefab, transform.position + Vector3.right * 2, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(_explosionPrefab, transform.position + Vector3.down * 2, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale *= 4;
    }

    /*IEnumerator Appear()
    {
        while (transform.position.y > 2.5f)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(Move());

    }

    IEnumerator Move()
    {
        while (true) { 

            while (transform.position.x > -6)
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
                yield return new WaitForSeconds(0.01f);
            }

            while (transform.position.x < 6)
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
                yield return new WaitForSeconds(0.01f);
            }
        }

    }*/

    public bool IsSecondForm()
    {
        return isSecondForm;
    }

    private void SecondForm()
    {
        _anim.SetTrigger("SecondForm");
        _audio.Play();
    }

    IEnumerator SecondFormRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            _anim.SetTrigger("preMegaBlaster");
            yield return new WaitForSeconds(3.1f);
            ScreenEffects.instance.Flash();
            yield return new WaitForSeconds(0.2f);
            Shake.instance.Shake2();
            _anim.SetTrigger("megaBlaster");
            yield return new WaitForSeconds(7);
        }
    }

}
