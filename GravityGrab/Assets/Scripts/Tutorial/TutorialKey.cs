using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKey : MonoBehaviour
{
    private float amplitude = 10f; // How far the image moves
    private float frequency = 5f; // How fast the image jiggles
    private RectTransform rectTransform;
    private Vector3 originalPosition;

    void Start()
    {
        amplitude = Random.Range(0.7f, 1.4f);
        frequency = Random.Range(4, 6.5f);
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.localPosition;
    }

    void Update()
    {
        // Calculate a jiggle effect
        float offsetX = Mathf.Sin(Time.time * frequency) * amplitude;
        float offsetY = Mathf.Cos(Time.time * frequency * 1.2f) * amplitude;

        // Apply the jiggle effect to the RectTransform
        rectTransform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);
    }
}
