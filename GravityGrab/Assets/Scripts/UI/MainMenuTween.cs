using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTween : MonoBehaviour
{
    [Header("UIElements")]
    [SerializeField] GameObject title1;
    [SerializeField] GameObject title2;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;

    [Header("Tween")]
    [SerializeField] float initialTime;
    [SerializeField] Ease titlesEase;
    [SerializeField] float titlesTime;
    [SerializeField] Ease buttonsEase;
    [SerializeField] float buttonsTime;

    void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(initialTime);
        seq.Append(title1.transform.DOMoveX(-2000, titlesTime).SetRelative(true).SetEase(titlesEase));
        seq.Insert(initialTime + titlesTime * 0.7f, title2.transform.DOMoveX(-2000, titlesTime).SetRelative(true).SetEase(titlesEase));
        seq.AppendInterval(0.5f);
        seq.Append(button1.transform.DOMoveY(-1000, buttonsTime).SetRelative(true).SetEase(buttonsEase));
        seq.Append(button2.transform.DOMoveY(-1000, buttonsTime).SetRelative(true).SetEase(buttonsEase));
        seq.Append(button3.transform.DOMoveY(-1000, buttonsTime).SetRelative(true).SetEase(buttonsEase));
        seq.Play();
    }

}
