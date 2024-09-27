using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    CharacterMove move;
    CharacterAttack attack;
    float idleMoveX;
    float idleMoveY;

    void Start()
    {
        animator = GetComponent<Animator>();
        move = GetComponent<CharacterMove>();
        attack = GetComponent<CharacterAttack>();

    }

    void Update()
    {
        HandleAnimation();
        HandleAttack();

    }
    void Animate(float moveX, float moveY, int layer)
    {
        switch (layer)
        {
            case 0:
                animator.SetLayerWeight(2, 1);
                animator.SetLayerWeight(1, 0);
                break;

            case 1:
                animator.SetLayerWeight(1, 1);
                animator.SetLayerWeight(2, 0);
                break;
        }
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
    }
    void HandleAnimation()
    {
        if (move.moveDirection != Vector2.zero)
        {
            Animate(move.moveX, move.moveY, 1);
            idleMoveX = move.moveX;
            idleMoveY = move.moveY;
        }
        else
        {
            Animate(idleMoveX, idleMoveY, 0);
        }
    }
    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && attack.canShoot && attack.gunType == GunType.Pistol && attack.HandleChangeGun().Ammo > 0)
        {
            animator.SetFloat("ShootX", move.GetShootingDirection().x);
            animator.SetFloat("ShootY", move.GetShootingDirection().y);
            animator.SetBool("IsAttack", true); 

        }
        else if (Input.GetMouseButtonDown(0) && attack.canShoot && attack.gunType == GunType.Knife)
        {
            animator.SetFloat("ShootX", move.GetShootingDirection().x);
            animator.SetFloat("ShootY", move.GetShootingDirection().y);
            animator.SetTrigger("Melee");
            attack.Stab();
        }
        else if (!Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttack", false);
            attack.canShoot = true;
        }
    }

}
