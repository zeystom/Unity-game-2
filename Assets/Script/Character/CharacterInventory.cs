using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.UI;
using static UnityEditor.Progress;
public class CharacterInventory : MonoBehaviour
{
    public GameObject Inventory;
    public List<ItemScript> items;
    bool isOpened = false;
    List<Button> buttons;
    public GameObject button;
    public Transform parentBtn;
    CharacterStats playerStats;
    Items itemsControl;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<CharacterStats>();
        itemsControl = FindObjectOfType<Items>();


    }

    // Update is called once per frame
    void Update()
    {
        OpenInventory();
    }


    void OpenInventory()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowInventory();

        }
    }


    void ShowInventory()
    {
        if (!isOpened)
        {
            Inventory.SetActive(true);
            ClearItems();
            GenerateItems();
        }
        else
            Inventory.SetActive(false);

        isOpened = !isOpened;
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
    public void AddToInventory(ItemScript item)
    {
        if (items.Count <= 31)
        {
            items.Add(item); ClearItems();
            GenerateItems();
        }
    }
    void GenerateItems()
    {
        var index = items.Count;
        for (int i = 0; i < index; i++)
        {
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
    void BuyItem(int index)
    {

            switch (items[index].ItemType)
            {
                case ItemType.ARMOR:
                    itemsControl.CheckMax(ref playerStats.Armor, playerStats.MaxArmor, items[index].Value, false);
                Destroy(parentBtn.GetChild(index).gameObject);
                items.Remove(items[index]);
                ClearItems();
                GenerateItems();
                break;
                case ItemType.MEDECINE:
                    itemsControl.CheckMax(ref playerStats.Hp, playerStats.MaxHp, items[index].Value, false);
                items.Remove(items[index]);
                Destroy(parentBtn.GetChild(index ).gameObject);
                ClearItems();
                GenerateItems();
                break;

            }
        }
    }
