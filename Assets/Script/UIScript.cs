using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;

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

    void Start()
    {
        stats = FindObjectOfType<CharacterStats>();
        characterAttack = FindObjectOfType<CharacterAttack>();
        opacity = KnifeFrameICO.color;
        opacity.a = 0.5f;
        noOpacity = KnifeFrameICO.color;
        noOpacity.a = 1.0f;
    }

    void Update()
    {
        InitializeStats();
    }


    public void OnChangeGun(GunType gunType)
    {
        if (gunType == GunType.Knife) 
        {
            KnifeFrame.color = Color.red;
            CrossbowFrame.color = Color.white;
            PistolFrame.color = Color.white;
            
            passiveColor(KnifeFrameICO, PistolFrameICO, CrossbowFrameICO);

        }
        else if (gunType == GunType.Pistol)
        {
            KnifeFrame.color = Color.white;
            CrossbowFrame.color = Color.white;
            PistolFrame.color = Color.red;

            passiveColor(PistolFrameICO, KnifeFrameICO, CrossbowFrameICO);

        }
        else
        {
            KnifeFrame.color = Color.white;
            CrossbowFrame.color = Color.red;
            PistolFrame.color = Color.white;

            passiveColor(CrossbowFrameICO, KnifeFrameICO, PistolFrameICO);
        }
    }


    void passiveColor(Image headImage, Image secondaryImage, Image secondaryImage2)
    {
        headImage.color = noOpacity;
        secondaryImage.color = opacity;
        secondaryImage2.color = opacity;
    }



    public void ReloadCircleToNull()
    {
        PistolFrame.fillAmount = 0;
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
            ammoText.text = characterAttack.HandleChangeGun().Ammo + "/" + characterAttack.HandleChangeGun().MaxAmmo;
        }
        moneyText.text = "$ " + stats.Money;
    }
  
}
