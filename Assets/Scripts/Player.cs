﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float hpCurrent;
    [SerializeField] private float hpMax;
    [SerializeField] private float hpExtra;

    //MOVIMIENTO
    [SerializeField] private float xSpeed = 5;
    [SerializeField] private float ySpeed = 3;
    [SerializeField] private float maxXSpeed = 3;


    //PLAYER STATS
    [SerializeField] private float damageBase;
    [SerializeField] private float fireDmg;
    [Tooltip("PERCENTAGE")]
    [SerializeField] private float blockBase = 50;

    //JUMPING
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float maxKinEnergy = 10;
    [SerializeField] private float minKinEnergy = 4;
    private float jumpFeetInitPosY;
    private float jumpMaxPosY;
    private float jumpCurrentPosY;
    private float jumpInitPosY;

    private bool isUp = false;
    private bool isJumping = false;
    private bool isBlocking = false;
    private bool isFiring = false;
    private bool isAttacking = false;
    private bool canMove = true;
    private bool fireIsUp = true;
    private bool regen = false;

    private float tFireCd = 0;
    [SerializeField] private float fireCd = 2f;
    private float regenAmount = 0;

    [SerializeField] private Transform topLimit = null;
    [SerializeField] private Transform bottomLimit = null;
    [SerializeField] private Transform leftLimit = null;
    [SerializeField] private Transform rightLimit = null;

    private Transform feet;
    private PlayerSword sword;
    private PlayerFire fire;

    //MOVEMENT INTERNAL VARIABLES
    private float horizontalAux = 0;
    private float verticalAux = 0;

    //ITEM VARIABLES
    private bool isOnItem = false;
    int id = -1;

    public bool IsInvincible { get; set; } = false;
    public bool IsOnItem { get => isOnItem; }
    public float HpCurrent { get => hpCurrent; set => hpCurrent = value; }
    public float HpMax { get => hpMax; set => hpMax = value; }
    public float XSpeed { get => xSpeed; set => xSpeed = value; }
    public float YSpeed { get => ySpeed; set => ySpeed = value; }
    public float MaxXSpeed { get => maxXSpeed; set => maxXSpeed = value; }
    public float DamageBase { get => damageBase; set => damageBase = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }
    public bool IsFiring { get => isFiring; set => isFiring = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsBlocking { get => isBlocking; set => isBlocking = value; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }
    public bool FireIsUp { get => fireIsUp; }
    public float TFireCd { get => tFireCd; set => tFireCd = value; }

    SpriteRenderer spriteRenderer;
    GameObject itemObject;

    private void Start()
    {
        ((InputManager)InputManager.Instance).OnPlayerHorizontal += MoveHorizontal;
        ((InputManager)InputManager.Instance).OnPlayerVertical += MoveVertical;
        ((InputManager)InputManager.Instance).OnPlayerAttack += Attack;
        ((InputManager)InputManager.Instance).OnPlayerFire += Fire;
        ((InputManager)InputManager.Instance).OnPlayerBlock += Block;
        ((InputManager)InputManager.Instance).OnPlayerStopBlocking += StopBlocking;
        ((InputManager)InputManager.Instance).OnPlayerJump += Jump;

        feet = transform.Find("Feet");
        sword = GetComponentInChildren<PlayerSword>(true);
        fire = GetComponentInChildren<PlayerFire>(true);

        sword.InitDmg(damageBase);
        fire.InitDmg(fireDmg);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void MoveHorizontal(int axis)
    {
        Vector3 auxPos = transform.position;
        bool outOfLeftBound = auxPos.x < leftLimit.position.x;
        bool outOfRightBound = auxPos.x > rightLimit.position.x;
        if (!outOfLeftBound && !outOfRightBound)
        {
            horizontalAux = XSpeed * axis * Time.deltaTime;
        }
        else
        {
            horizontalAux = 0;
            if (outOfLeftBound)
            {
                auxPos.x = leftLimit.position.x;
            }
            else if (outOfRightBound)
            {
                auxPos.x = rightLimit.position.x;
            }
            transform.position = auxPos;
        }
    }
    private void MoveVertical(int axis)
    {
        verticalAux = YSpeed * axis * Time.deltaTime;

        if (!isJumping && (feet.transform.position.y + verticalAux >= topLimit.position.y || feet.transform.position.y + verticalAux <= bottomLimit.position.y))
        {
            verticalAux = 0;
        }
    }

    private void Attack()
    {
        if (!IsOnItem)
        {
            if (!isBlocking && !IsFiring && !isAttacking)
            {
                sword.gameObject.SetActive(true);
                isAttacking = true;
            }
        }
        else
        {
            ((ItemManager)ItemManager.Instance).ApplyItem(this, id);
            isOnItem = false;
            Destroy(itemObject);
        }
    }
    private void Fire()
    {
        if (fireIsUp)
        {
            if (!isJumping && !isBlocking && !isAttacking && !IsFiring)
            {
                canMove = false;
                fire.gameObject.SetActive(true);
                isFiring = true;
                fireIsUp = false;
            }
        }
    }
    private void Block()
    {
        if (!isJumping && !IsFiring && !isAttacking && !isBlocking)
        {
            canMove = false;
            isBlocking = true;
        }
    }
    private void StopBlocking()
    {
        canMove = true;
        isBlocking = false;
    }


    private void Jump()
    {
        if (!isJumping)
        {
            jumpFeetInitPosY = feet.position.y;

            jumpMaxPosY = transform.position.y + jumpForce;
            jumpCurrentPosY = transform.position.y;
            jumpInitPosY = transform.position.y;

            isJumping = true;
            isUp = true;
        }
    }


    private void Update()
    {
        if (isJumping)
        {
            if (canMove)
            {
                //KINETIC ENERGY CALCULATIONS
                float kinEnergy = Mathf.Lerp(maxKinEnergy, minKinEnergy, (jumpCurrentPosY - jumpInitPosY) / (jumpMaxPosY - jumpInitPosY)) * Time.deltaTime;

                if (jumpCurrentPosY < jumpMaxPosY && isUp)
                {
                    transform.position = new Vector3(transform.position.x + horizontalAux, transform.position.y + kinEnergy, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x + horizontalAux, transform.position.y - kinEnergy, transform.position.z);
                    isUp = false;
                }
                jumpCurrentPosY = transform.position.y;
                //

                //SUM THE VERTICAL OFFSET CREATED BY OUR INPUT MID-JUMP
                if (jumpFeetInitPosY <= topLimit.position.y && jumpFeetInitPosY >= bottomLimit.position.y)
                {
                    jumpFeetInitPosY += verticalAux;
                    jumpInitPosY += verticalAux;
                    jumpMaxPosY += verticalAux;
                }
                //

                if (feet.position.y <= jumpFeetInitPosY)
                {
                    isJumping = false;
                    transform.position = new Vector3(transform.position.x, jumpInitPosY, transform.position.z);
                }
            }
        }
        //
        else if (horizontalAux != 0 || verticalAux != 0)
        {
            if (canMove)
            {
                transform.Translate(horizontalAux, verticalAux, 0);
            }
        }

        if (regen)
        {
            if (hpCurrent < hpMax)
            {
                hpCurrent += regenAmount * Time.deltaTime;
            }
            else
            {
                hpCurrent = hpMax;
            }
        }

        if (!fireIsUp)
        {
            tFireCd += Time.deltaTime;
            if (tFireCd >= fireCd)
            {
                tFireCd = 0;
                fireIsUp = true;
            }
        }
    }

    public void SetInvincible(bool setter)
    {
        IsInvincible = setter;
        spriteRenderer.color = setter ? Color.green : Color.white;
    }

    public void ChangeHp(float aux)
    {
        if (aux < 0)
        {
            if (!IsInvincible)
            {
                if (isBlocking)
                {
                    aux *= blockBase / 100;
                }
            }
            AudioSource.PlayClipAtPoint(SoundManager.Instance.playerHit, transform.position);

        }

        hpCurrent += aux;

        if (hpCurrent <= 0)
        {
            GameManager.Instance.EndGame(false);
        }
    }
    public void ChangeDmg(float c)
    {
        damageBase += c;
        sword.InitDmg(damageBase);
    }
    private void DeactivateSword()
    {
        isAttacking = false;
        sword.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemIdInScene itemId = collision.GetComponent<ItemIdInScene>();
        if (itemId != null)
        {
            id = itemId.Id;
            if (id >= 0)
            {
                isOnItem = true;
            }

            itemObject = collision.gameObject;
        }

        RegenZone rZone = collision.GetComponent<RegenZone>();
        if (rZone)
        {
            regenAmount = rZone.RegenAmount;
            regen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ItemIdInScene itemId = collision.GetComponent<ItemIdInScene>();
        if (itemId != null)
        {
            id = itemId.Id;
            if (id >= 0)
            {
                isOnItem = false;
                id = -1;
            }

            itemObject = null;
        }

        RegenZone rZone = collision.GetComponent<RegenZone>();
        if (rZone)
        {
            regen = false;
        }
    }
}
