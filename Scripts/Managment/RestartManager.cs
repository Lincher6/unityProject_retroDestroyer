using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("Restart"))
        {
            SceneManager.LoadScene(1);
        }
    }
}
