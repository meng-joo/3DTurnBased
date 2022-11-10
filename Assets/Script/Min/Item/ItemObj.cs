using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ItemType : int
{
    Helmet,
    Armor,
    Pants,
    Shoes,
    Weapon,
    Jewelry
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/New Item")]
public class ItemObj : ScriptableObject
{
    public ItemType itemType;
    public bool getFlagStackable; //겹칠수있는가

    public Sprite itemIcon;
    public GameObject objModelPrefab; //장착을할모델

    public Item itemData = new Item(); //아이템 데이타 생성

    public List<string> boneNameLists = new List<string>(); 

    [TextArea(15, 20)]
    public string itemSummery;

    private void OnValidate()
    {
        boneNameLists.Clear();

        //장착형 아이템이 아니면 돌아가
        if (objModelPrefab == null || objModelPrefab.GetComponentInChildren<SkinnedMeshRenderer>() == null)
        {
            return;
        }

        SkinnedMeshRenderer renderer = objModelPrefab.GetComponentInChildren<SkinnedMeshRenderer>();

        var bones = renderer.bones;

        foreach (var t in bones)
        {
            boneNameLists.Add(t.name); //뼈대 이름을 넣는다
        }
    }

    public Item createItemObj()
    {
        Item new_Item = new Item(this);
        return new_Item;
    }
}