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
    private bool isFiring = false;
    private bool isAttacking = false;
    private bool isBlocking = false;
    private bool isAirAttacking = false;

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
        ((InputManager)InputManager.Instance).OnPlayerFire += Fire;

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
        if (m_player.CanMove)
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

    }

    protected override void Attack()
    {
        if (!m_player.IsOnItem && !isAttacking)
        {
            Debug.Log("yikes");
            if (!isJumping)
            {
                isAttacking = true;
                m_animator.SetTrigger("Attack");
            }
            else
            {
                isAirAttacking = true;
                m_animator.SetTrigger("JumpAttack");
            }
        }
    }
    private void SetIsAttackingFalse()
    {
        isAttacking = false;
        m_animator.ResetTrigger("Attack");
    }
    private void SetIsAirAttackingFalse()
    {
        isAirAttacking = false;
        m_animator.ResetTrigger("JumpAttack");
    }

    protected void Fire()
    {
        if (!isAttacking && !isJumping && !isBlocking && !isFiring && !isAirAttacking)
        {
            isFiring = true;
            m_animator.SetTrigger("RangedAttack");
        }
    }
    private void SetIsFiringFalse()
    {
        isFiring = false;
        m_animator.ResetTrigger("RangedAttack");
    }

    protected override void Block()
    {
        //Debug.Log(!isAttacking && !isJumping && !isFiring && !isBlocking && !isAirAttacking);
        if (!isAttacking && !isJumping && !isFiring && !isBlocking && !isAirAttacking)
        {
            isBlocking = true;
            m_animator.SetBool("Block", true);
        }
    }
    protected override void StopBlocking()
    {
        isBlocking = false;
        m_animator.SetBool("Block", false);
    }

    protected override void Jump()
    {
        if (!isJumping && !isAttacking && !isBlocking && !isFiring && !isAirAttacking)
        {
            isJumping = true;
            m_animator.SetTrigger("Jump");
        }
    }
    private void SetIsJumpingFalse()
    {
        m_animator.ResetTrigger("Jump");
        isJumping = false;
    }
}
