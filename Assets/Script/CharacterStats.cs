using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour

{
    public int Hp { get; set; }
    public int MaxHp { get; set; } = 100;
    public int Experience { get; set; } = 0;
    public int Level { get; set; } = 1;
    public int Armor { get; set; }
    public int MaxArmor { get; set; } = 100;

    public int Money { get; set; } = 0;

    CharacterStats()
    {
        this.Hp = this.MaxHp;
  
    }

}
