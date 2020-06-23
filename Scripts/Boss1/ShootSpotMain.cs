using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpotMain : MonoBehaviour
{
    [SerializeField]
    private GameObject _regularProjectile;
    void Start()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            for (int i = -45; i < 50; i += 15)
            {
                Instantiate(_regularProjectile, transform.position, Quaternion.Euler(0, 0, i));
            }
        }
    }
}
