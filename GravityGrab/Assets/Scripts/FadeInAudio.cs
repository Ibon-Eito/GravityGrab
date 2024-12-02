using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FadeInAudio : MonoBehaviour
{
    private AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        //audioS.volume = 0;
        //StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic()
    {
        while (audioS.volume < 0.1f)
        {
            audioS.volume += 0.005f;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
