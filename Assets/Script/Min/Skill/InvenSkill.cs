using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class InvenSkill : MonoBehaviour
{
    public static bool isFirst = true;

    public AllSkills allSkills;

    public Card card;

    public Transform cardParentTrm;

    public Button exitBtn;

    public Transform skillDeckTrm;
    public Transform contentTrm;

    public SkillIInvenObj skillInvenObj;
    public SkillIInvenObj skillDeckInvenObj;

    public Card[] skillCards;

    public List<Skill> tempSkill;
    public void EndInven()
    {
        Click.isSelected = false;
        if (Click.clickCard)
        {
            Click.clickCard.SetAlpha(0f);
        }
        //Click.clickCard = null;
    }
    private void Awake()
    {
      

        SetDeck();
    }
    public void SetDeck()
    {
        for (int i = 0; i < skillDeckInvenObj.cards.Count; i++)
        {
            skillCards[i].Skill = skillDeckInvenObj.cards[i];
        }
    }
    public void CreateFirst()
    {
        for (int i = 0; i < allSkills._allSkills.Length; i++)
        {
            GameObject cardObj = Instantiate(card, Vector3.zero, Quaternion.identity).gameObject;
            cardObj.GetComponent<Card>().Skill = allSkills._allSkills[i];
            cardObj.transform.SetParent(cardParentTrm);
            skillInvenObj.cards.Add(cardObj.GetComponent<Card>().Skill);
        }
            Debug.Log("첫번쨰");
    }
    public void DefaultCreate()
    {
        for (int i = 0; i < skillInvenObj.cards.Count; i++)
        {
            GameObject cardObj = Instantiate(card, Vector3.zero, Quaternion.identity).gameObject;
            cardObj.GetComponent<Card>().Skill = skillInvenObj.cards[i];
            cardObj.transform.SetParent(cardParentTrm);
        }
            Debug.Log("기본");
    }

    public void DeleteCard()
    {
        RectTransform[] childList = cardParentTrm.GetComponentsInChildren<RectTransform>();
        foreach (var deletecard in childList)
        {
            if (deletecard == cardParentTrm)
                continue;

            Destroy(deletecard.gameObject);
        }
    }

    private void Update()
    {
    }
}
