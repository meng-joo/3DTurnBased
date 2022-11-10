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
    public bool getFlagStackable; //��ĥ���ִ°�

    public Sprite itemIcon;
    public GameObject objModelPrefab; //�������Ҹ�

    public Item itemData = new Item(); //������ ����Ÿ ����

    public List<string> boneNameLists = new List<string>(); 

    [TextArea(15, 20)]
    public string itemSummery;

    private void OnValidate()
    {
        boneNameLists.Clear();

        //������ �������� �ƴϸ� ���ư�
        if (objModelPrefab == null || objModelPrefab.GetComponentInChildren<SkinnedMeshRenderer>() == null)
        {
            return;
        }

        SkinnedMeshRenderer renderer = objModelPrefab.GetComponentInChildren<SkinnedMeshRenderer>();

        var bones = renderer.bones;

        foreach (var t in bones)
        {
            boneNameLists.Add(t.name); //���� �̸��� �ִ´�
        }
    }

    public Item createItemObj()
    {
        Item new_Item = new Item(this);
        return new_Item;
    }
}