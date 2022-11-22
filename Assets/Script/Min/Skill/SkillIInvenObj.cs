using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillInterfaceType
{
    Deck,
    Inventory
}

[CreateAssetMenu(menuName = "Inven/SkillInventory")]
public class SkillIInvenObj : ScriptableObject
{
    public SkillInterfaceType typeInven;

    public List<Skill> cards;
    //µ¦¸®½ºÆ®¸¦
}
