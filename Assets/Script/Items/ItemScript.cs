using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewItem", menuName = "Items")]

public class ItemScript : MonoBehaviour
{
    public ItemType ItemType;
    public string Name;
    public int Value;
    public int Cost;
    public Sprite sprite;
    public GameObject ItemPrefab;
}