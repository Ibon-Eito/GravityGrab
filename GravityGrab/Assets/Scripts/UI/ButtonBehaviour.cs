using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Tween")]
    [SerializeField] private float newScale = 1.05f;
    [SerializeField] private float tweenTime = 0.2f;
    [SerializeField] private Ease scaleEase = Ease.InOutQuad;

    private AudioSource audioSource;
    private Button button;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayButtonVFX);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.DOScale(new Vector3(newScale, newScale, 1), tweenTime).SetEase(scaleEase).Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.DOScale(new Vector3(1, 1, 1), tweenTime).SetEase(scaleEase).Play();
    }

    public void PlayButtonVFX()
    {
        audioSource.Play();
    }
}
