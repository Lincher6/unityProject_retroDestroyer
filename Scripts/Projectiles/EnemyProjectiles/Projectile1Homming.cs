using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1Homming : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _rotation = 300;
    [SerializeField]
    private GameObject _damagePrefab;

    public Vector3 origin;
    public Vector3 target;

    void Start()
    {
        origin = transform.position;
        target = PlayerMovement.instance.transform.position;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * _rotation * Time.deltaTime);
        transform.Translate((target - origin).normalized * _speed * Time.deltaTime, Space.World);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Instantiate(_damagePrefab, transform.position, Quaternion.identity);
            PlayerDamage.instance.Damage(1);
            Destroy(gameObject);
        }
    }
}
