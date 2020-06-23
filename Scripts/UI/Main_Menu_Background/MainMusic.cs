using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour
{
    public static MainMusic instance;

    private void Awake()
    {
        instance = this;
    }

    public void MusicStop()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
