using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    public ItemScript item;
    CharacterStats playerStats;
    CharacterAttack attack;
    CharacterInventory inventory;
    UIScript ui;
    void Start()
    {
        playerStats = FindObjectOfType<CharacterStats>();
        attack = FindObjectOfType<CharacterAttack>();
        ui = FindObjectOfType<UIScript>();
        inventory = FindObjectOfType<CharacterInventory>();
    }

    void Update()
    {


    }

    public void LevelUp(int value)
    {
        if (playerStats.Experience >= playerStats.MaxExperience || playerStats.Experience + value >= playerStats.MaxExperience)
        {
            playerStats.Experience += value;
            playerStats.Level++;
            playerStats.Experience -= playerStats.MaxExperience;
            UpgradeStats();
            playerStats.MaxExperience = (int)(playerStats.Level * 0.5f * (playerStats.MaxExperience + playerStats.MaxExperience * 0.1f));
            ui.FillExp(playerStats.MaxExperience);
        }
        else
        {
            playerStats.Experience += value;
        }
    }

    public void UpgradeStats()
    {
        var multiplierHp = Mathf.Pow(playerStats.Level, 0.3f) * playerStats.MaxHp;
        playerStats.MaxHp = (int)multiplierHp;
        var multiplierArm = Mathf.Pow(playerStats.Level, 0.3f) * playerStats.MaxArmor;
        playerStats.MaxArmor = (int)multiplierArm;
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            switch (item.ItemType)
            {
                case ItemType.ARMOR:
                    //CheckMax(ref playerStats.Armor, playerStats.MaxArmor, item.Value, true  );
                    inventory.AddToInventory(item);  
                    
                    break;
                case ItemType.MEDECINE:
                    //CheckMax(ref playerStats.Hp, playerStats.MaxHp, item.Value, true);
                    inventory.AddToInventory(item);

                    break; 
                case ItemType.MONEY:
                    CheckMax(ref playerStats.Money, 931231232, item.Value, true);
                    break;
                case ItemType.EXP:
                    LevelUp(item.Value);
                    Destroy(gameObject);
                    break;
                case ItemType.ARROWS:
                    var gun = FindGun(attack.weapons, ItemType.ARROWS);
                    CheckMax(ref gun.MaxAmmo, gun.LimitAmmo, item.Value,true);
                    break;
                case ItemType.BULLETS:
                     var gun1 = FindGun(attack.weapons, ItemType.BULLETS);
                    CheckMax(ref gun1.MaxAmmo, gun1.LimitAmmo, item.Value, true);
                    break;

            }

        }

    }
    public  WeaponScript FindGun(List<WeaponScript> weapons, ItemType bulletType)
        {
            foreach (var weapon in weapons)
            {
                if (weapon.BulletType == bulletType) 
                {
                    
                    return weapon;
                };
            }
            WeaponScript wew = new WeaponScript();
            return wew;
        }

        public void CheckMax(ref int stat, int maxStat, int value, bool bob)
    {
        if (stat != maxStat&& bob)
        {
            Destroy(gameObject);
        }

        if (stat >= maxStat || stat + value >= maxStat)
            {
                stat = maxStat;
            }
            else
            {
                stat += value;

            }

    }



}
