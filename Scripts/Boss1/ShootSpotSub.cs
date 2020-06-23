using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpotSub : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    void Start()
    {
        StartCoroutine(Shot1());
    }

    IEnumerator Shot1()
    {
        while (!Boss1.instance.IsSecondForm())
        {
            yield return new WaitForSeconds(5);
            for (int i = 0; i < 3; i++)
            {
                Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }

            
        }
    }
}
