using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pivot : MonoBehaviour
{
    [SerializeField] private PlayerInput input;

    void Start()
    {
        InputSystem.pollingFrequency = 144;
    }

    void Update()
    {
        if(!GameState.paused)
        {
            float rotationZ = 0f;
            if(input.currentControlScheme.Equals("Gamepad"))
            {
                rotationZ = Mathf.Atan2(PlayerVariables.playerController.lookInput.y, PlayerVariables.playerController.lookInput.x) * Mathf.Rad2Deg;
            }
            else
            {
                Vector3 difference = Camera.main.ScreenToWorldPoint(PlayerVariables.playerController.lookInput) - transform.position;
                difference.Normalize();
                rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            }
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
    }

    public void InputChangeHandler(PlayerInput _input)
    {
        Debug.Log(_input.currentControlScheme);
        if(_input.currentControlScheme.Equals("Keyboard"))
            InputSystem.EnableDevice(Mouse.current);
        else
            InputSystem.DisableDevice(Mouse.current);
    }
}
