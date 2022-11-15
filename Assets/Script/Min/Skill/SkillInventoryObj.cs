using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "New SkillInvnetoryObject", menuName = "Inventory/SkillInvnetoryObject")]
public class SkillInventoryObj : ScriptableObject
{
    public AllSkills allSkills;

    [SerializeField]
    private SkillInventory skillInventory = new SkillInventory();

    public List<SkillInventorySlot> skillInventories => skillInventory.skillInventorySlots;


    public Action<SkillInventoryObj> OnUseItemObj;

    public void Clear()
    {
        skillInventories.Clear();
    }
}
