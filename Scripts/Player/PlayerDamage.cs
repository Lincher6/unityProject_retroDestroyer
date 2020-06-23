using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private int _hp;
    private int _lives;
    private bool _isShieldActive = false;
    private float _invinciblityTime;
    private float _startPosition;
    private AudioSource _audio;

    [SerializeField]
    private GameObject _shieldVisual;
    [SerializeField]
    private GameObject _damageRight, _damageLeft;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _playerExplosionPrefab;

    private UIManager _uiManager;
    public static PlayerDamage instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        _hp = 3;
        _lives = 3;
        _startPosition = -3;
        transform.position = new Vector3(0, _startPosition, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audio = transform.GetComponent<AudioSource>();
    }

    public void Damage(int power)
    {
        _audio.Play();
        ScreenEffects.instance.RedFlash();

        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisual.SetActive(false);
            if (power > 2)
            {
                Death();
                return;
            }
            else
            {
                return;
            }
        }

        _hp -= power;
        transform.GetComponent<PlayerFire>().PowerDecrease();

        if (_hp == 2)
        {
            _damageRight.SetActive(true);
        }
        else if (_hp == 1)
        {
            _damageLeft.SetActive(true);
        }

        if (_hp <= 0)
        {
            _damageRight.SetActive(false);
            _damageLeft.SetActive(false);
            Death();
        }
    }

    void Death()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
        _lives--;
        _uiManager.UpdateLives(_lives);
        _hp = 3;
        transform.GetComponent<PlayerMovement>().SpeedDecrease();
        transform.GetComponent<PlayerFire>().PowerDecrease();

        _invinciblityTime = Time.time + 2;
        StartCoroutine(NewStart(_invinciblityTime));
        transform.position = new Vector3(0, _startPosition, 0);

        if (_lives <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator NewStart(float time)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        while (Time.time < time)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.05f);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.05f);
        }

        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
     
    public void ShieldEnable()
    {
        _isShieldActive = true;
        _shieldVisual.SetActive(true);
    }
}
