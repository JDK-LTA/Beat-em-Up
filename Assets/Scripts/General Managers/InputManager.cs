using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public delegate void OnInputMovement(int axis);
    public event OnInputMovement OnPlayerVertical;
    public event OnInputMovement OnPlayerHorizontal;

    public delegate void OnInputTrigger();
    public event OnInputTrigger OnPlayerAttack;
    public event OnInputTrigger OnPlayerFire;
    public event OnInputTrigger OnPlayerBlock;
    public event OnInputTrigger OnPlayerStopBlocking;
    public event OnInputTrigger OnPlayerJump;

    private bool isMovingHor = false;
    private bool isMovingVer = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            OnPlayerHorizontal?.Invoke(-1);
            isMovingHor = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            OnPlayerHorizontal?.Invoke(1);
            isMovingHor = true;
        }
        //DUDAS SOBRE OPTIMIZACIÓN
        else if (isMovingHor && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            OnPlayerHorizontal?.Invoke(0);
            isMovingHor = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            OnPlayerVertical?.Invoke(-1);
            isMovingVer = true;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            OnPlayerVertical?.Invoke(1);
            isMovingVer = true;
        }
        else if (isMovingVer && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            OnPlayerVertical?.Invoke(0);
            isMovingVer = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnPlayerAttack?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnPlayerFire?.Invoke();
        }
        if (Input.GetMouseButton(1))
        {
            OnPlayerBlock?.Invoke();
        }
        else if(Input.GetMouseButtonUp(1))
        {
            OnPlayerStopBlocking?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPlayerJump?.Invoke();
        }
    }
}
