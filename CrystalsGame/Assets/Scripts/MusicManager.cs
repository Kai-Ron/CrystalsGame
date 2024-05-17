using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicSource;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        musicSource = GetComponent<AudioSource>();
    }
    
    public void PlayMusic()
    {
        if (musicSource.isPlaying) return;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
