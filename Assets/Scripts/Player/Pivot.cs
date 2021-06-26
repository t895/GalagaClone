using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        if(!GameState.paused)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(PlayerVariables.playerController.lookInput) - transform.position;
            difference.Normalize();

            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
    }
}
