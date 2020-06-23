using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    private float _spawnPosition;
    private int _powerup = 0;
    private bool _bossAlive = true;

    [SerializeField]
    public GameObject _regularSpaceship;
    [SerializeField]
    public GameObject _regularSpaceshipShooting;
    [SerializeField]
    public GameObject _enemy1;
    [SerializeField]
    public GameObject[] _asteroids;
    [SerializeField]
    public GameObject _asteroidTurret;
    [SerializeField]
    public GameObject _asteroidBig;
    [SerializeField]
    public GameObject _cautionPrefab;
    [SerializeField]
    public GameObject _spaceStation;
    [SerializeField]
    public GameObject _boss;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    public GameObject[] _powerups;

    public static SpawnManager instance;

    void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        ScreenEffects.instance.Appear();
        _spawnPosition = 7;
        StartCoroutine(SpawnWave1());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnWave1()
    {
        yield return new WaitForSeconds(3);

        for (int i = 0; i < 10; i++)
        {
            SpawnRegularSpaceship();
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnEnemy1(-4));
        yield return new WaitForSeconds(5);
        StartCoroutine(SpawnEnemy1(4));
        yield return new WaitForSeconds(5);
        StartCoroutine(SpawnEnemy1(0));
        yield return new WaitForSeconds(5);
        StartCoroutine(SpawnEnemy1(-4));
        StartCoroutine(SpawnEnemy1(4));
        yield return new WaitForSeconds(5);

        for (int i = 0; i < 15; i++)
        {
            SpawnRegularSpaceshipShooting();
            yield return new WaitForSeconds(1);

            if (i % 10 == 0)
            {
                StartCoroutine(SpawnEnemy1(4));
            }
            else if (i % 5 == 0)
            {
                StartCoroutine(SpawnEnemy1(-4));
            }
        }

        StartCoroutine(SpawnWave2());
    }

    ////// WAVE 2 Asteroids

    IEnumerator SpawnWave2()
    {
        yield return new WaitForSeconds(4);

        for (int i = 1; i < 50; i++)
        {
            SpawnAsteroid(Random.Range(-8, 8));
            yield return new WaitForSeconds(0.5f);

            if (i % 25 == 0)
            {
                StartCoroutine(SpawnEnemy1(-4));
            }
            else if (i % 40 == 0)
            {
                StartCoroutine(SpawnEnemy1(4));
            }

            if (i % 10 == 0)
            {
                SpawnRegularSpaceshipShooting();
            }
        }

        for (int i = 0; i < 70; i++)
        {
            SpawnAsteroid(Random.Range(-11, -5));
            yield return new WaitForSeconds(0.3f);
            SpawnAsteroid(Random.Range(5, 11));
            yield return new WaitForSeconds(0.3f);

            if (i % 15 == 0)
            {
                SpawnAsteroidTurret(Random.Range(-5, 5));
            }

            if (i % 3 == 0)
            {
                SpawnRegularSpaceshipShooting();
            }
        }

        StartCoroutine(SpawnWave3());
    }

    ////// wave 3 Hyperdrive
    IEnumerator SpawnWave3()
    {
        yield return new WaitForSeconds(5);
        transform.GetComponent<AudioSource>().Play();
        PlayerMovement.instance.HyperdriveOn();
        for (int i = 0; i < 30; i++)
        {
            Background.instance._speedMultiplier += 0.25f;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2);

        StartCoroutine(SpawnObstacle(5));
        yield return new WaitForSeconds(4);
        StartCoroutine(SpawnObstacle(-7));
        StartCoroutine(SpawnObstacle(-2));
        yield return new WaitForSeconds(4);
        StartCoroutine(SpawnObstacle(-7));
        StartCoroutine(SpawnObstacle(7));
        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnObstacle(0));
        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnObstacle(7));
        StartCoroutine(SpawnObstacle(2));
        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnObstacle(-7));
        StartCoroutine(SpawnObstacle(-2));
        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnObstacle(-7));
        StartCoroutine(SpawnObstacle(7));
        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnObstacle(7));
        yield return new WaitForSeconds(1);
        StartCoroutine(SpawnObstacle(0));
        yield return new WaitForSeconds(1);
        StartCoroutine(SpawnObstacle(-7));
        yield return new WaitForSeconds(3);




        PlayerMovement.instance.HyperdriveOff();
        while(Background.instance._speedMultiplier > 1)
        {
            Background.instance._speedMultiplier -= 0.25f;
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(SpawnWave4());

    }

    ////// wave 4 boss
    
    IEnumerator SpawnWave4()
    {
        yield return new WaitForSeconds(2);
        Instantiate(_spaceStation, new Vector3(4, _spawnPosition + 1, 0), Quaternion.identity);
        yield return new WaitForSeconds(4);
        MusicManager1.instance.Stop("main");
        MusicManager1.instance.Play("alert");
        yield return new WaitForSeconds(8);
        MusicManager1.instance.Play("boss");
        Instantiate(_boss, new Vector3(0, _spawnPosition + 2, 0), Quaternion.Euler(0, 0, 180));

        while (_bossAlive)
        {
            yield return new WaitForSeconds(1);
        }

        MusicManager1.instance.Stop("boss");
        yield return new WaitForSeconds(2);
        MusicManager1.instance.Play("stageClear");
        yield return new WaitForSeconds(6);

        SceneManager.LoadScene(1);
    }

    ////// end of level

    IEnumerator SpawnPowerupRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(12);
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 7, 0);
            Instantiate(_powerups[_powerup], posToSpawn, Quaternion.identity);
            if (_powerup < 3)
            {
                _powerup++;
            }
            else
            {
                _powerup = 0;
            }
        }
    }

    void SpawnRegularSpaceship()
    {

        Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), _spawnPosition, 0);
        GameObject newEnemy = Instantiate(_regularSpaceship, posToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;

    }

    void SpawnRegularSpaceshipShooting()
    {

        Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), _spawnPosition, 0);
        GameObject newEnemy = Instantiate(_regularSpaceshipShooting, posToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;

    }

    void SpawnAsteroid(float xPos)
    {

        Vector3 posToSpawn = new Vector3(xPos, _spawnPosition, 0);
        GameObject newEnemy = Instantiate(_asteroids[Random.Range(0, _asteroids.Length)], posToSpawn, Quaternion.identity);
        newEnemy.transform.localScale = newEnemy.transform.localScale * Random.Range(1, 1.3f);
        newEnemy.transform.parent = _enemyContainer.transform;

    }

    void SpawnAsteroidTurret(float xPos)
    {

        Vector3 posToSpawn = new Vector3(xPos, _spawnPosition + 2, 0);
        GameObject newEnemy = Instantiate(_asteroidTurret, posToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;

    }

    void SpawnCaution(float xPos)
    {

        Vector3 posToSpawn = new Vector3(xPos, 2, 0);
        GameObject newEnemy = Instantiate(_cautionPrefab, posToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;

    }

    void SpawnAsteroidBig(float xPos)
    {

        Vector3 posToSpawn = new Vector3(xPos, _spawnPosition + 3, 0);
        GameObject newEnemy = Instantiate(_asteroidBig, posToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;

    }

    public void BossKilled()
    {
        _bossAlive = false;
    }

    IEnumerator SpawnObstacle(float xPos)
    {
        Vector3 posToSpawn = new Vector3(xPos, 2, 0);
        GameObject newEnemy = Instantiate(_cautionPrefab, posToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;

        yield return new WaitForSeconds(1.5f);

        posToSpawn = new Vector3(xPos, _spawnPosition + 3, 0);
        newEnemy = Instantiate(_asteroidBig, posToSpawn, Quaternion.identity);
        newEnemy.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        newEnemy.transform.parent = _enemyContainer.transform;

    }

    IEnumerator SpawnEnemy1(float xPosition)
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(_enemy1, new Vector3(xPosition, _spawnPosition, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

    }
}
