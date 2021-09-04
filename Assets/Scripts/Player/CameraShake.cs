using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private void Awake()
    {
        PlayerVariables.cameraShake = this;
    }

    public IEnumerator Shake(float _duration, float _magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;
        
        do
        {
            float x = Random.Range(-1f, 1f) * _magnitude;
            float y = Random.Range(-1f, 1f) * _magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        while(elapsed <= _duration);
        
        transform.localPosition = originalPosition;
    }
    
}
