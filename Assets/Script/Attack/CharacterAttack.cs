using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public bool canShoot = true;
    public GunType gunType = GunType.Pistol;
    float lastAttackedAt = -9999f;
    [SerializeField] GameObject bulletPref;
    public List<WeaponScript> weapons;
    [SerializeField] float cooldown = 0.5f;
    [Header("References")]
    [Header("Melee Attack")]
    [SerializeField] Transform meleeAttackPoint;
    [SerializeField] float meleeAttackRange = 1f;
    CharacterMove playerMove;
    CharacterStats stats;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<CharacterMove>();
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleChangeGun();
    }
    public void Bullet()
    {
        if (canShoot)
        {
            Instantiate(HandleChangeGun().ProjPrefab, transform.position + playerMove.GetShootingDirection(), Quaternion.identity);
            canShoot = false;
            HandleChangeGun().Ammo -= 1;
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


    public void Reload()
    {

    }


    public WeaponScript HandleChangeGun()
    {
        WeaponScript weapon1 = null;
        if (Input.GetKeyDown(KeyCode.Alpha1))
            gunType = GunType.Crossbow;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            gunType = GunType.Pistol;

        foreach(var weapon in weapons)
        {
            if(weapon.GunType == gunType)
            {
                weapon1 = weapon;
            }
        }
        return weapon1;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(meleeAttackPoint.position, meleeAttackRange);
    }
}
