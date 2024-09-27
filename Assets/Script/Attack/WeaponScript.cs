using System.Collections;
using System.Collections.Generic;
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
}
