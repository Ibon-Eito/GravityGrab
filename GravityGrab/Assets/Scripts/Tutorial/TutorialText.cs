using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public void ShowText()
    {
        transform.DOScale(new Vector3(1,1,1),0.2f).SetEase(Ease.InOutQuad).Play();
    }

    public void HideText()
    {
        transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.InOutQuad).Play();
    }
}
