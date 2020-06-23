using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _planets;
    private int _number = 0;
    private Vector3 _position;
    private float _sizeMultiplier;
    private float _spawnRate;

    public float _speedMultiplier = 1;

    public static Background instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(PlanetsMovement());

    }

    IEnumerator PlanetsMovement()
    {
        while(true)
        {
            _spawnRate = Random.Range(13, 20);
            yield return new WaitForSeconds(_spawnRate);

            _position = new Vector3(Random.Range(-9, 9), 10, 0);
            _sizeMultiplier = Random.Range(0.3f, 1);

            GameObject planet = Instantiate(_planets[_number], _position, Quaternion.identity);
            planet.transform.localScale *= _sizeMultiplier;

            if (_number < 3)
            {
                _number++;
            }
            else
            {
                _number = 0;
            }
        }
    }
}
