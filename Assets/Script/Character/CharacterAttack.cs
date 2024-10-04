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
    CharacterActions playerMove;
    CharacterStats stats;
    UIScript ui;
    bool isReloading = false;
    float reloadTimer = 0f;
    public bool swapBlock = false;


    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<CharacterActions>();
        stats = GetComponent<CharacterStats>();
        ui = FindObjectOfType<UIScript>();
        ui.OnChangeGun(gunType);
    }

    // Update is called once per frame
    void Update()
    {
        HandleChangeGun();
        Reload();
        
    }


    private void OnDestroy()
    {
        weapons.ForEach(weapon => weapon.ResetValues());
    }

    public void Bullet()
    {
        if (canShoot)
        {
            isReloading = false;
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




     void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (HandleChangeGun().Ammo != HandleChangeGun().MagazineSize)
            {
                isReloading = true;
                reloadTimer = 0f;
            }
        }

        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > HandleChangeGun().ReloadTime)
            {
                CompleteReload();
            }
        }
    }
    
    void CompleteReload()
    {

        var weapon = HandleChangeGun();

        int currentMagazSize = weapon.Ammo;
        int availableAmmo = weapon.MaxAmmo;
        int neededAmmo = weapon.MagazineSize - currentMagazSize;
        if (availableAmmo >= neededAmmo)
        {

            weapon.Ammo = weapon.MagazineSize;
            weapon.MaxAmmo -= neededAmmo;
        }
        else
        {
            weapon.Ammo += availableAmmo;
            weapon.MaxAmmo = 0;
        }

        isReloading = false;
    }
    
    public WeaponScript HandleChangeGun()
    {
        WeaponScript weapon1 = null;

   

        if (Input.GetKeyDown(KeyCode.Alpha3) && !swapBlock)
        {
            gunType = GunType.Crossbow;
            isReloading = false;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && !swapBlock)
        {
            gunType = GunType.Pistol;
            isReloading = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && !swapBlock)
        {
            gunType = GunType.Knife;
            isReloading = false;
        }
 

        ui.OnChangeGun(gunType);
        foreach (var weapon in weapons)
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