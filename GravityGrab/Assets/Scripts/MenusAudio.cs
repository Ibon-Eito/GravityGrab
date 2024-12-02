using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusAudio : MonoBehaviour
{
    public static MenusAudio Instance { get; private set; }

    private AudioSource musicSource;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PauseMenuMusic()
    {
        StartCoroutine(FadeOutMusic());
    }

    public void ResumeMenuMusic()
    {
        if (musicSource.volume == 0)
        {
            StartCoroutine(FadeInMusic());
        }
    }

    private IEnumerator FadeInMusic()
    {
        while(musicSource.volume< 0.1f)
        {
            musicSource.volume += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator FadeOutMusic()
    {
        while (musicSource.volume > 0)
        {
            musicSource.volume -= 0.02f;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
