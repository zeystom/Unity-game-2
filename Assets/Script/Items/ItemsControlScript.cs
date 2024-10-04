using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    public ItemScript item;
    CharacterStats playerStats;
    void Start()
    {
        playerStats = FindObjectOfType<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (item.ItemType)
            {
                case ItemType.ARMOR:
                playerStats.Armor += item.Value;
                break;
                    case ItemType.MEDECINE:
                playerStats.Hp += item.Value;
                break;
                    case ItemType.MONEY:
                playerStats.Money += item.Value;
                break;
                    case ItemType.EXP:
                playerStats.Experience += item.Value;
                    break;
                case ItemType.ARROWS:
                    playerStats.Armor += item.Value;
                    break;
                case ItemType.BULLETS:
                    playerStats.Armor += item.Value;
                    break;

            }
        }

        WeaponScript FindGun(List<WeaponScript> weapons, ItemType bulletType)
        {
            foreach (var weapon in weapons)
            { 
                if(weapon.BulletType == bulletType) return weapon;
            }
            WeaponScript wew = new WeaponScript();
            return wew;
        }

    }
}
