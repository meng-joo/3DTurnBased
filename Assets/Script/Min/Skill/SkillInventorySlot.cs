using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class SkillInventorySlot
{

    public SkillType[] skillTypes = new SkillType[0];

    [NonSerialized]
    public SkillInventoryObj skillInventoryObj;

    [NonSerialized]
    public GameObject slotUI;

    [NonSerialized]
    public Action<SkillInventorySlot> OnPreUpload;
    [NonSerialized]
    public Action<SkillInventorySlot> OnPostUpload;

    public SkillInventoryObj SkillObject
    {
        get
        {
            return skillInventoryObj;
        }
    }

    public Skill skill;
    public SkillInventorySlot() => uploadSlot(new Skill());
    public SkillInventorySlot(Skill skill) => uploadSlot(skill);

    public void destoryItem() => uploadSlot(new Skill());


    public void uploadSlot(Skill skill)
    {
        skill = new Skill();

        OnPreUpload?.Invoke(this);
        this.skill = skill;
        OnPostUpload?.Invoke(this);
    }
 
}
