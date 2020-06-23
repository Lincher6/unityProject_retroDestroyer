using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidTurretShot : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    void Start()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(2);

        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(_projectilePrefab, transform.position, transform.rotation);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2);
        }
    }
}
