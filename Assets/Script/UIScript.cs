using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{

    CharacterStats stats;
    CharacterAttack characterAttack;
    [Header("Bars")]
    [SerializeField] Slider hpBar;
    [SerializeField] Slider armorBar;
    [SerializeField] Slider expBar;


    [Header("Stats")]
    [SerializeField] TMP_Text lvlText;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text ammoText;

    void Start()
    {
        stats = FindObjectOfType<CharacterStats>();
        characterAttack = FindObjectOfType<CharacterAttack>();
    }

    void Update()
    {
        InitializeStats();
    }

    void InitializeStats()
    {

        hpBar.value = stats.Hp;
        armorBar.value = stats.Armor;
        expBar.value = stats.Experience;
        lvlText.text = stats.Level.ToString();
        ammoText.text = characterAttack.HandleChangeGun().Ammo + "/" + characterAttack.HandleChangeGun().MaxAmmo;
        moneyText.text = "$ " + stats.Money;
    }
}
