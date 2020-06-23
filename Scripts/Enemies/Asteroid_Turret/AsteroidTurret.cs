using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidTurret : MonoBehaviour
{
    [SerializeField]
    private int _hp = 15;
    private bool isDead = false;
    [SerializeField]
    private int _damageToPlayer = 10;
    [SerializeField]
    private int _scoreOnDestroy = 100;

    private float _screenBound;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _asteroidPrefab;

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
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        if (!isDead)
        {
            isDead = true;
            UIManager.instance.UpdateScore(_scoreOnDestroy);

            StartCoroutine(DeathRoutine());

            Destroy(gameObject, 0.7f);
        }
    }

    IEnumerator DeathRoutine()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(_explosionPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(_explosionPrefab, transform.position + Vector3.left * 1.5f, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(_explosionPrefab, transform.position + Vector3.right * 1.5f, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(_explosionPrefab, transform.position + Vector3.down * 1.5f, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale *= 3;

        GameObject asteroid1 = Instantiate(_asteroidPrefab, transform.position, transform.rotation);
        asteroid1.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-300, 0), 100, 0) * Time.deltaTime;
        GameObject asteroid2 = Instantiate(_asteroidPrefab, transform.position, transform.rotation);
        asteroid2.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-150, 150), 100, 0) * Time.deltaTime;
        GameObject asteroid3 = Instantiate(_asteroidPrefab, transform.position, transform.rotation);
        asteroid3.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(0, 300), 100, 0) * Time.deltaTime;
    }
    
}
