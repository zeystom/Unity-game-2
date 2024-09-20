using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float cooldown = 0.5f; 
    [Header("References")]
    [SerializeField] GameObject bulletPref;

    CharacterController controller;
    public Vector2 moveDirection { get; set; }
    public Vector3 mouseDirection;

    float idleMoveX;
    float idleMoveY;
    public float moveX { get; set; } 
    public float moveY { get; set; }

    public bool canShoot = true;
    public GunType gunType = GunType.Pistol;
    float lastAttackedAt = -9999f; 

    [Header("Melee Attack")]
    [SerializeField] Transform meleeAttackPoint;
    [SerializeField] float meleeAttackRange = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleChangeGun();
        
    }

    void FixedUpdate()
    {
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    

    public Vector3 GetShootingDirection()
    {
        mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDirection.z = 0f;
        return (mouseDirection - transform.position).normalized;
    }

    void HandleMovementInput()

    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    public void Bullet()
    {
        if (canShoot)
        {
            GameObject bulletInstance = Instantiate(bulletPref, transform.position + GetShootingDirection(), Quaternion.identity);
            canShoot = false;
        }
    }

    public void Stab()
    {
        Collider2D enemy = Physics2D.OverlapCircle(meleeAttackPoint.position, meleeAttackRange);
        if (enemy != null)
        {
            Debug.Log("stab!");
        }
        else
        {
            Debug.Log("stabik");
        }
    }

    void HandleChangeGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            gunType = GunType.Knife;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            gunType = GunType.Pistol;

        Debug.Log(gunType);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(meleeAttackPoint.position, meleeAttackRange);
    }
}