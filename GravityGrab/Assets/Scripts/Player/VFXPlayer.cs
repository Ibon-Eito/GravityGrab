using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VFXPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> orbClips;
    [SerializeField] AudioClip keyClip;
    [SerializeField] AudioClip damageClip;
    [SerializeField] AudioClip stepClip;

    private AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void PlayOrbSFX(int orbsCaptured)
    {
        audioS.PlayOneShot(orbClips[Mathf.Min(orbsCaptured, orbClips.Count-1)]);
    }

    public void PlayKeySFX()
    {
        audioS.PlayOneShot(keyClip);
    }
    public void PlayDamageSFX()
    {
        audioS.PlayOneShot(keyClip);
    }

    public void PlayStetSFX()
    {
        StartCoroutine(PlayStepSFXRoutine());
    }

    private IEnumerator PlayStepSFXRoutine()
    {
        audioS.pitch = Random.Range(1f, 1.8f);
        audioS.PlayOneShot(stepClip);
        yield return new WaitForSeconds(stepClip.length);
        audioS.pitch = 1;
    }
}
