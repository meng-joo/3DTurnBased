using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillInventory 
{
    public List<SkillInventorySlot> skillInventorySlots = new List<SkillInventorySlot>();

    public void Clear()
    {
        foreach (SkillInventorySlot skillSlot in skillInventorySlots)
        {
            skillSlot.destoryItem();
        }
    }

}
