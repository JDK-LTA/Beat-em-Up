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
    public event OnInputTrigger OnPlayerBlock;
    public event OnInputTrigger OnPlayerStopBlocking;
    public event OnInputTrigger OnPlayerJump;

    private bool isMovingHor = false;
    private bool isMovingVer = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            OnPlayerHorizontal(-1);
            isMovingHor = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            OnPlayerHorizontal(1);
            isMovingHor = true;
        }
        //DUDAS SOBRE OPTIMIZACIÓN
        else if (isMovingHor && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            OnPlayerHorizontal(0);
            isMovingHor = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            OnPlayerVertical(-1);
            isMovingVer = true;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            OnPlayerVertical(1);
            isMovingVer = true;
        }
        else if (isMovingVer && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            OnPlayerVertical(0);
            isMovingVer = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnPlayerAttack();
        }
        if (Input.GetMouseButton(1))
        {
            OnPlayerBlock();
        }
        else if(Input.GetMouseButtonUp(1))
        {
            OnPlayerStopBlocking();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPlayerJump();
        }
    }
}
