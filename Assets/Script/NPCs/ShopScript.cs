using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopScript : MonoBehaviour
{
    public GameObject Shop;
    public bool inShop = false;
    public List<ItemScript> items;
    List<Button> buttons;
    public GameObject button;
    GameObject spawnText;
    public Transform parentBtn;
    public Transform spawnPos;
    public GameObject text;
    CharacterAttack attack;
    Items itemsControl;
    CharacterStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<CharacterStats>();
        itemsControl = FindObjectOfType<Items>();
        attack = FindObjectOfType<CharacterAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenShop();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PressE();
            inShop = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inShop = false;
            ClearItems();
            Destroy(spawnText);
            spawnText = null;
        }
        Shop.SetActive(false);
    }

    void OpenShop()
    {
        if (Input.GetKeyDown(KeyCode.E) && inShop)
        {
            Shop.SetActive(true);

            ClearItems();

            GenerateItems();


        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inShop)
        {
            Shop.SetActive(false);
            ClearItems();

        }

    }

    void GenerateItems()
    {
        var index = items.Count;
        for (int i = 0; i < index; i++) {
            {
                var a = Instantiate(button, parentBtn);
                a.GetComponentInChildren<TMP_Text>().text = items[i].Cost.ToString();
                a.GetComponentInChildren<Image>().sprite = items[i].sprite;
                int btnIndex = i;
                Debug.Log(a.name + i);
                Debug.Log(items[i].ItemType);
                a.GetComponent<Button>().onClick.AddListener(() => { BuyItem(btnIndex); });
            }
        } 
    }
    void ClearItems()
    {
        int a = parentBtn.childCount;
        while (a > 0)
        {
            Destroy(parentBtn.GetChild(a - 1).gameObject);
            a--;
        }
    }
    
    void BuyItem(int index)
    {
        Debug.Log("ind" + index);
        Debug.Log(playerStats.Money + "denga");
        if (playerStats.Money >= items[index].Cost)
        {
            switch (items[index].ItemType)
            {
                case ItemType.ARMOR:
                    itemsControl.CheckMax(ref playerStats.Armor, playerStats.MaxArmor, items[index].Value, false);
                    break;
                case ItemType.MEDECINE:
                    itemsControl.CheckMax(ref playerStats.Hp, playerStats.MaxHp, items[index].Value, false);
                    break;
  
                case ItemType.ARROWS:
                    var gun = itemsControl.FindGun(attack.weapons, ItemType.ARROWS);
                    itemsControl.CheckMax(ref gun.MaxAmmo, gun.LimitAmmo, items[index].Value, false);
                    break;
                case ItemType.BULLETS:
                    var gun1 = itemsControl.FindGun(attack.weapons, ItemType.BULLETS);
                    itemsControl.CheckMax(ref gun1.MaxAmmo, gun1.LimitAmmo, items[index].Value, false);
                    break;
            }
            playerStats.Money -= items[index].Cost;
        }
    }

    void PressE()
    {
        spawnText = Instantiate(text, spawnPos.position, Quaternion.identity);
    }

}
