using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "NewWeapon",menuName = "Weapons")]
public class WeaponScript : ScriptableObject
{
    public int Ammo;
    public int MaxAmmo;
    public int LimitAmmo;
    public int MagazineSize;
    public int Damage;
    public float AttackSpeed;
    public float ReloadTime;
    public GunType GunType;
    public GameObject ProjPrefab;
    public ItemType BulletType;

    private int initalAmmo;
    private int initalMaxAmmo;
    private int initalLimitAmmo;
    private int initalMagazineSize;
    private int initalDamage;
    private float initalAttackSpeed;
    private float initalReloadTime;

    void SaveInitialValues()
    {
        initalAmmo = Ammo;
        initalMaxAmmo = MaxAmmo;
        initalLimitAmmo = LimitAmmo;
        initalMagazineSize = MagazineSize;
        initalDamage = Damage;
        initalAttackSpeed = AttackSpeed;
        initalReloadTime = ReloadTime;
    }


    public void ResetValues()
    {
        Ammo = initalAmmo;
        MaxAmmo = initalMaxAmmo;
        LimitAmmo = initalLimitAmmo;
        MagazineSize = initalMagazineSize;
        Damage = initalDamage;
        AttackSpeed = initalAttackSpeed;
        ReloadTime = initalReloadTime;
    }
        
    private void OnEnable()
    {
        SaveInitialValues();
    }

}
