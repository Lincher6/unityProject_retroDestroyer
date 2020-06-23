using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    private float _speed;
    private float _speedMultiplier;
    private float _speedLimit;
    private float _boundLeft = -10;
    private float _boundRight = 10;
    private float _boundUp = 5.5f;
    private float _boundDown = -5.5f;
    private Animator _anim;

    public static PlayerMovement instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        _speed = 5;
        _speedMultiplier = 1.3f;
        _speedLimit = 11;
        _anim = GetComponent<Animator>();
}

    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetMouseButton(0))
        {
            Vector3 position = Input.mousePosition;
            position.z = 10;
            position.y += 120;
            position = Camera.main.ScreenToWorldPoint(position);
            if ((position - transform.position).magnitude > 0.3f)
            {
                transform.Translate((position - transform.position).normalized * 15 * Time.deltaTime);
            }

        }
#else
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput < 0) _anim.SetBool("turnLeft", true);
        else _anim.SetBool("turnLeft", false);
        if (horizontalInput > 0) _anim.SetBool("turnRight", true);
        else _anim.SetBool("turnRight", false);

        if (Input.GetMouseButton(0))
        {
            Vector3 position = Input.mousePosition;
            position.z = 10;
            position = Camera.main.ScreenToWorldPoint(position);
            if ((position - transform.position).magnitude > 0.1f)
            {
                transform.Translate((position - transform.position).normalized * _speed * Time.deltaTime);
            }

        }
        else
        {
            Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(direction * _speed * Time.deltaTime);
            Vector3 boundaries = new Vector3(Mathf.Clamp(transform.position.x, _boundLeft, _boundRight), Mathf.Clamp(transform.position.y, _boundDown, _boundUp), transform.position.z);
            transform.position = boundaries;
        }
#endif
    }

    public void SpeedIncrease()
    {
        if (_speed < _speedLimit)
        {
            _speed *= _speedMultiplier;
            Thruster.instance.ScaleUp();
        }
    }

    public void SpeedDecrease()
    {
        _speed = 4;
        Thruster.instance.ScaleDown();
    }

    public void HyperdriveOn()
    {
        _anim.SetBool("isHyperdrive", true);
    }

    public void HyperdriveOff()
    {
        _anim.SetBool("isHyperdrive", false);
    }
}
