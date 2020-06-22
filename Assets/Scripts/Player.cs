using System.Collections;
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

    [SerializeField] private Transform topLimit = null;
    [SerializeField] private Transform bottomLimit = null;

    private Transform feet;

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

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        ((InputManager)InputManager.Instance).OnPlayerHorizontal += MoveHorizontal;
        ((InputManager)InputManager.Instance).OnPlayerVertical += MoveVertical;
        ((InputManager)InputManager.Instance).OnPlayerAttack += Attack;
        ((InputManager)InputManager.Instance).OnPlayerBlock += Block;
        ((InputManager)InputManager.Instance).OnPlayerStopBlocking += StopBlocking;
        ((InputManager)InputManager.Instance).OnPlayerJump += Jump;

        feet = transform.Find("Feet");

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void MoveHorizontal(int axis)
    {
        horizontalAux = XSpeed * axis * Time.deltaTime;
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

        }
        else
        {
            ((ItemManager)ItemManager.Instance).ApplyItem(this, id);
        }
    }
    private void Block()
    {
        isBlocking = true;
    }
    private void StopBlocking()
    {
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
        //
        else if (horizontalAux != 0 || verticalAux != 0)
        {
            transform.Translate(horizontalAux, verticalAux, 0);
        }

        if (IsInvincible)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void ChangeHp(float aux)
    {
        if (aux < 0)
        {
            if (isBlocking)
            {
                aux *= blockBase / 100;
            }
        }

        hpCurrent += aux;
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
        }
    }
}
