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

    [Header("Icons")]
    [SerializeField] Image KnifeFrame;
    [SerializeField] Image PistolFrame;
    [SerializeField] Image CrossbowFrame;

    [Header("Images")]
    [SerializeField] Image KnifeFrameICO;
    [SerializeField] Image PistolFrameICO;
    [SerializeField] Image CrossbowFrameICO;

    [Header("Stats")]
    [SerializeField] TMP_Text lvlText;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text ammoText;

    Color opacity;
    Color noOpacity;

    Dictionary<GunType, (Image frame, Image icon)> gunDictionary;

    void Start()
    {
        stats = FindObjectOfType<CharacterStats>();
        characterAttack = FindObjectOfType<CharacterAttack>();

        opacity = KnifeFrameICO.color;
        opacity.a = 0.5f;
        noOpacity = KnifeFrameICO.color;
        noOpacity.a = 1.0f;

        gunDictionary = new Dictionary<GunType, (Image, Image)>
        {
            { GunType.Knife, (KnifeFrame, KnifeFrameICO) },
            { GunType.Pistol, (PistolFrame, PistolFrameICO) },
            { GunType.Crossbow, (CrossbowFrame, CrossbowFrameICO) }
        };
    }

    void Update()
    {
        InitializeStats();
    }

    public void OnChangeGun(GunType gunType)
    {
        foreach (var gun in gunDictionary)
        {
            if (gun.Key == gunType)
            {
                gun.Value.frame.color = Color.red;
                gun.Value.icon.color = noOpacity;

            }
            else
            {
                gun.Value.frame.color = Color.white;
                gun.Value.icon.color = opacity;

            }

        }
    }

    public void FillCircleProgressive(float progress, GunType gunType)
    {

        foreach (var gun in gunDictionary)
        {
            if (gun.Key == gunType)
            {
                gun.Value.frame.fillAmount = Mathf.Clamp01(progress);

            }

        } 
    }
    public void FillCircleOnce()
    {
        foreach (var gun in gunDictionary)
        {
            gun.Value.frame.fillAmount = 1;
        }
    }

    public void FillExp(int progress)
    {
        expBar.maxValue = progress;
        hpBar.maxValue = stats.MaxHp;
        armorBar.maxValue = stats.MaxArmor;
    }



    void InitializeStats()
    {
        hpBar.value = stats.Hp;
        armorBar.value = stats.Armor;
        expBar.value = stats.Experience;
        lvlText.text = stats.Level.ToString();


        if (characterAttack.HandleChangeGun().GunType == GunType.Knife)
        {
            ammoText.text = "";
        }
        else
        {
            ammoText.text = $"{characterAttack.HandleChangeGun().Ammo}/{characterAttack.HandleChangeGun().MaxAmmo}";
        }

        moneyText.text = "$ " + stats.Money;
    }
}
