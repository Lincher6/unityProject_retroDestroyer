using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager1 : MonoBehaviour
{
    [SerializeField]
    private AudioSource _mainMusic;
    [SerializeField]
    private AudioSource _alert;
    [SerializeField]
    private AudioSource _bossTheme;
    [SerializeField]
    private AudioSource _stageClear;
    [SerializeField]
    private AudioSource _pause;

    public static MusicManager1 instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Play(string name)
    {
        switch (name)
        {
            case "main": _mainMusic.Play(); break;
            case "alert": _alert.Play(); break;
            case "boss": _bossTheme.Play(); break;
            case "stageClear": _stageClear.Play(); break;
            case "pause": _pause.Play(); break;
        }
    }

    public void Stop(string name)
    {
        switch (name)
        {
            case "main": _mainMusic.Stop(); break;
            case "alert": _alert.Stop(); break;
            case "boss": _bossTheme.Stop(); break;
        }
    }

}
