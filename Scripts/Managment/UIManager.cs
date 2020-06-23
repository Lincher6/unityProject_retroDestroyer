using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private int _lives;
    [SerializeField]
    private Text _livesText;
    [SerializeField]
    private Text _scoreText;
    private int _score = 0;
    [SerializeField]
    private GameObject _gameOver;
    [SerializeField]
    private GameObject _restart;
    [SerializeField]
    private GameObject _restartManagerPrefab;
    [SerializeField]
    private GameObject _pauseMenuPrefab;

    public static bool isPaused = false;
    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        _lives = 3;
        _scoreText.text = "Score: " + _score;
        _livesText.text = "X" + _lives;
        _gameOver.SetActive(false);
        _restart.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
           
        }
    }

    public void UpdateScore(int newScore)
    {
        _score += newScore;
        _scoreText.text = "Score: " + _score;
    }

    public void UpdateLives(int lives)
    {
        _lives = lives;
        _livesText.text = "X" + _lives;

        if (_lives <= 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        Instantiate(_restartManagerPrefab, transform.position, Quaternion.identity);
        _gameOver.SetActive(true);
        _restart.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true) { 
            _gameOver.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOver.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        _pauseMenuPrefab.SetActive(true);
        MusicManager1.instance.Play("pause");
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        _pauseMenuPrefab.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
