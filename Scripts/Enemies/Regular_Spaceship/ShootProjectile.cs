using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private float _speed = 30;
    private Animator _anim;
    private RegularSpaceship instace;

    void Start()
    {
        instace = transform.GetComponent<RegularSpaceship>();
        _anim = transform.GetComponent<Animator>();
        StartCoroutine(ShootingRoutine());
    }

    IEnumerator ShootingRoutine()
    {
        GameObject bullet;
        _anim.SetTrigger("preProjectile");
        yield return new WaitForSeconds(1);

        if (instace.isAive())
        {
            bullet = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(2);

        if (instace.isAive())
        {
            _anim.SetTrigger("preProjectile");
            yield return new WaitForSeconds(1);
        }

        if (instace.isAive())
        {
            bullet = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        }
        
    }
}
