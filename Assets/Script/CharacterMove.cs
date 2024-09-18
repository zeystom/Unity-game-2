using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float cooldown = 1f; // Использую старую переменную cooldown
    [Header("References")]
    [SerializeField] GameObject bulletPref;

    CharacterController controller;
    Animator animator;
    Vector2 moveDirection;
    Vector3 mouseDirection;

    float idleMoveX;
    float idleMoveY;
    float moveX;
    float moveY;

    bool canShoot = true;
    int gunType = 0;

    float lastAttackedAt = -9999f; // Перенос из старого кода

    [Header("Melee Attack")]
    [SerializeField] Transform meleeAttackPoint;
    [SerializeField] float meleeAttackRange = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleAnimation();
        HandleChangeGun();

        if (Input.GetMouseButtonDown(0) && canShoot) 
            StartCoroutine(Attack());
    }

    void FixedUpdate()
    {
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void Animate(float moveX, float moveY, int layer)
    {
        switch (layer)
        {
            case 0:
                animator.SetLayerWeight(0, 1);
                animator.SetLayerWeight(1, 0);
                break;

            case 1:
                animator.SetLayerWeight(1, 1);
                animator.SetLayerWeight(0, 0);
                break;
        }
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
    }

    Vector3 GetShootingDirection()
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

    void HandleAnimation()
    {
        if (moveDirection != Vector2.zero)
        {
            Animate(moveX, moveY, 1);
            idleMoveX = moveX;
            idleMoveY = moveY;
        }
        else
        {
            Animate(idleMoveX, idleMoveY, 0);
        }
    }

    public void Shoot()
    {
        Vector3 shootingDirection = GetShootingDirection();
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        GameObject bulletInstance = Instantiate(bulletPref, transform.position + shootingDirection, Quaternion.identity);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = shootingDirection * 10f;
        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Destroy(bulletInstance, 4f);
    }

    IEnumerator Attack()
    {
        canShoot = false;

        animator.SetFloat("ShootX", GetShootingDirection().x);
        animator.SetFloat("ShootY", GetShootingDirection().y);

        switch (gunType)
        {
            case 0:
                animator.SetBool("IsAttack", true);
                break;

            case 1:
                animator.SetTrigger("Melee");
                Collider2D enemy = Physics2D.OverlapCircle(meleeAttackPoint.position, meleeAttackRange);
                Debug.Log(enemy.name);
                break;
        }

        yield return new WaitForSeconds(cooldown);

        animator.SetBool("IsAttack", false);
        canShoot = true;

    }

    void HandleChangeGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            gunType = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            gunType = 1;

        Debug.Log(gunType);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(meleeAttackPoint.position, meleeAttackRange);
    }
}