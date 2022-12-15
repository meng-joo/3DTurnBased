using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public enum ItemType : int
{
    Helmet,
    Armor,
    Pants,
    Shoes,
    Weapon,
    Jewelry,
    Default
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/New Item")]
public class ItemObj : ScriptableObject
{
    [Header("������ �Լ��̸�")]
    public string[] _methodName;


    public ItemType itemType;
    public bool getFlagStackable; //��ĥ���ִ°�

    public Sprite itemIcon;
    public GameObject objModelPrefab; //�������Ҹ�

    public Item itemData = new Item(); //������ ����Ÿ ����

    public List<string> boneNameLists = new List<string>(); 

    [TextArea(15, 20)]
    public string itemSummery;


    private SetItem setItem = new SetItem();
    public SetItem _SetSkill => setItem;

    public int value;

    private void OnEnable()
    {
        SetItem(this);
    }

    void SetItem(ItemObj _itemInfo)
    {
        setItem.AddEvent(_itemInfo);
    }

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

public class SetItem
{
    private ItemFunc _itemSkill;
    private List<MethodInfo> methods = new List<MethodInfo>();

    public List<MethodInfo> _Methods => methods;

    public void AddEvent(ItemObj itemInfo)
    {
        Type type = typeof(ItemFunc);
        for (int i = 0; i < itemInfo._methodName.Length; i++)
        {
            MethodInfo method = type.GetMethod(itemInfo._methodName[i]);
            methods.Add(method);
        }
    }

    void CallEvent(GameObject enemy)
    {
        foreach (var method in methods)
        {
            method.Invoke(null, new object[] { enemy });
        }
    }
}