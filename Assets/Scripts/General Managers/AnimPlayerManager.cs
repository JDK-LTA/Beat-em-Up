using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimPlayerManager : AnimationManager
{
    Animator m_animator;
    private float initXScale;
    Player m_player;

    private bool isJumping = false;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_player = GetComponent<Player>();
    }

    private void Start()
    {
        ((InputManager)InputManager.Instance).OnPlayerHorizontal += Move;
        ((InputManager)InputManager.Instance).OnPlayerAttack += Attack;
        ((InputManager)InputManager.Instance).OnPlayerBlock += Block;
        ((InputManager)InputManager.Instance).OnPlayerStopBlocking += StopBlocking;
        ((InputManager)InputManager.Instance).OnPlayerJump += Jump;

        initXScale = transform.localScale.x;
    }

    protected override void Idle()
    {
        m_animator.SetTrigger("Idle");
    }

    protected override void Climb()
    {

    }

    //ASIGNAR EL MISMO MÉTODO A DOS EVENTOS?? BIEN O MAL? GENERA CONFLICTOS DE LLAMADAS?? CONTROL CON BOOL?
    protected override void Move(int axis)
    {
        if (axis == 0)
        {
            m_animator.SetBool("Run", false);
            //m_animator.ResetTrigger("Run");
            m_animator.SetTrigger("Idle");
            //Debug.Log("Idle");
        }
        else
        {
            transform.localScale = new Vector3(initXScale * axis, transform.localScale.y, transform.localScale.z);
            //Debug.Log("Run");
            m_animator.SetBool("Run", true);
            //m_animator.SetTrigger("Run");
        }

    }

    protected override void Attack()
    {
        if (!m_player.IsOnItem)
        {
            m_animator.SetTrigger("Attack");
        }
    }

    protected override void Block()
    {
        m_animator.SetBool("Block", true);
    }

    protected override void StopBlocking()
    {
        m_animator.SetBool("Block", false);
    }

    //COMO HACER PARA SEPARAR EL INICIO, EL MEDIO Y EL FINAL DEL SALTO CON SINGLETON. PLAYER INICIA EVENTOS? BUSCO EL RIGIDBODY DESDE AQUI? UN COLLIDER EN LOS PIES?
    protected override void Jump()
    {
        if (!isJumping)
        {
            m_animator.SetTrigger("Jump");
            isJumping = true;
        }
    }
    private void SetIsJumpingFalse()
    {
        isJumping = false;
    }
}
