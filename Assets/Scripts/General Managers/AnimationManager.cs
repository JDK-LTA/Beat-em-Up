using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationManager : MonoBehaviour 
{
    protected abstract void Idle();
    protected abstract void Move(int axis);
    protected abstract void Climb();
    protected abstract void Attack();
    protected abstract void Block();
    protected abstract void StopBlocking();
    protected abstract void Jump();
}