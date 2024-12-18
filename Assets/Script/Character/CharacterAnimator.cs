using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    CharacterActions move;
    CharacterAttack attack;
    UIScript ui;
    ShopScript shop;
    float idleMoveX;
    float idleMoveY;

    void Start()
    {
        animator = GetComponent<Animator>();
        move = GetComponent<CharacterActions>();
        attack = GetComponent<CharacterAttack>();
        ui = FindObjectOfType<UIScript>();
        shop = FindObjectOfType<ShopScript>();

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
        if (shop.inShop)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && attack.canShoot && attack.gunType == GunType.Pistol && attack.HandleChangeGun().Ammo > 0)
        {

            attack.swapBlock = true;
            animator.SetFloat("ShootX", move.GetShootingDirection().x);
            animator.SetFloat("ShootY", move.GetShootingDirection().y);
            animator.SetTrigger("Pistol");
            animator.SetBool("IsAttack", true);

            attack.isReloading = false;
            ui.FillCircleOnce();
        }

        else if (Input.GetMouseButtonDown(0) && attack.canShoot && attack.gunType == GunType.Crossbow && attack.HandleChangeGun().Ammo > 0)
        {
            attack.swapBlock = true;
            animator.SetFloat("ShootX", move.GetShootingDirection().x);
            animator.SetFloat("ShootY", move.GetShootingDirection().y);
            animator.SetTrigger("Crossbow");
            animator.SetBool("IsAttack", true);

            attack.isReloading = false;
            ui.FillCircleOnce();

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
