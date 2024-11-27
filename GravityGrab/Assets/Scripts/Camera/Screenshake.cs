using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;

        float x, y;

        bool reduced50 = false, reduced70 = false, reduced90 = false;

        while (elapsed < duration)
        {
            if(elapsed >= duration*0.5f && !reduced50)
            {
                magnitude = magnitude / 2;
                reduced50 = true;
            }
            if(elapsed >= duration*0.7f && !reduced70)
            {
                magnitude = magnitude / 2;
                reduced70 = true;
            }
            if (elapsed >= duration * 0.9f && !reduced90)
            {
                magnitude = magnitude / 2;
                reduced90 = true;
            }

            x = Random.Range(-1f,1f) * magnitude;
            y = Random.Range(-1f, 1f) * magnitude;
            
            Vector2 dir = new Vector2(Physics2D.gravity.x * x, Physics2D.gravity.y * y);

            transform.localPosition = new Vector3(dir.x, dir.y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
