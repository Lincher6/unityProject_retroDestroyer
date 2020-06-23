using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerFire : MonoBehaviour
{
    // 0 - Laser
    // 1 - Wave
    // 2 - Missile
    private int _weapon;
    private int _power;
    private int _limit;
    private Vector3 _offSet;

    [SerializeField]
    private GameObject[] _lasers;
    [SerializeField]
    private GameObject[] _waves;
    [SerializeField]
    private GameObject[] _missiles;

    private GameObject[][] _weapons;

    private float _fireRate;
    private float _fireCool;

    public static PlayerFire instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        _weapon = 0;
        _power = 0;
        _limit = 2;
        _fireRate = 3;
        _offSet = new Vector3(0, 0.5f, 0);
        _weapons = new GameObject[][] { _lasers, _waves, _missiles };
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetMouseButton(0) && Time.time > _fireCool)
        {
            Fire();
        }
#else
        if (Input.GetButton("Fire1") && Time.time > _fireCool)
        {
            Fire();
        }
#endif
    }

    void Fire()
    {
        _fireCool = Time.time + 1 / _fireRate;
        Instantiate(_weapons[_weapon][_power], transform.position + _offSet, Quaternion.identity);
    }

    public void PowerIncrease()
    {
        if (_power < _limit)
        {
            _power++;
        }
    }

    public void PowerDecrease()
    {
        if (_power > 0)
        {
            _power--;
        }
    }

    public void ChangeWeapon(int weapon)
    {
        if (weapon <= _limit)
        {
            _weapon = weapon;
        }
    }
}
