using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void OnMusic()
    {
        //music.Play();
        AudioListener.volume = 1F;

    }
    public void OffMusic()
    {
        //music.Stop();
        AudioListener.volume = 0;
    }
}
