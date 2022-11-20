using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;

[Serializable]
public class InvenSlot
{
    public ItemType[] itemTypes = new ItemType[0];

    [NonSerialized]
    public InventoryObj inventoryObj;

    [NonSerialized]
    public GameObject slotUI;

    [NonSerialized]
    public Action<InvenSlot> OnPreUpload;
    [NonSerialized]
    public Action<InvenSlot> OnPostUpload;

    public Item item;
    public int itemCnt;
    public ItemObj ItemObject
    {
        get
        {
            return item.item_id >= 0 ? inventoryObj.itemDBObj.itemObjs[item.item_id] : null;
        }
    }
    private ApplyAbility applyAbility;

    public InvenSlot() => uploadSlot(new Item(), 0);
    public InvenSlot(Item item, int cnt) => uploadSlot(item, cnt);


    public void destoryItem() => uploadSlot(new Item(), 0);

    public void addCnt(int value) => uploadSlot(item, itemCnt += value);

    public void uploadSlot(Item item, int cnt)
    {
        Debug.Log("upload" + cnt);
        if (cnt <= 0)
        {
            item = new Item();
        }
        //플레이어값을바꿔야될꺼야
        OnPreUpload?.Invoke(this);
        this.item = item;
        this.itemCnt = cnt;
        OnPostUpload?.Invoke(this);


    }

    public bool getFlagEquipSlot(ItemObj itemObj)
    {
        if (itemTypes.Length <= 0 || itemObj == null || itemObj.itemData.item_id < 0)
        {
            return true;
        }

        foreach (ItemType itemType in itemTypes)
        {
            if (itemObj.itemType == itemType)
            {
                Type type = typeof(ApplyAbility);
                MethodInfo method = type.GetMethod("ChangeAbility");

                object[] obj = new object[1];
                obj[0] = itemObj.itemData.abilities;
                method.Invoke(applyAbility, obj);
                return true;
            }
        }

        return false;
    }
}