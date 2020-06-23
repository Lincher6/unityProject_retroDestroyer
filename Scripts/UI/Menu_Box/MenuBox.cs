using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBox : MonoBehaviour
{

    public void GameStart()
    {
        StartCoroutine(GameStarRoutine());
    }

    public void GameExit()
    {
        Application.Quit();
    }

    IEnumerator GameStarRoutine()
    {
        ScreenEffects.instance.Fade();
        MainMusic.instance.MusicStop();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
