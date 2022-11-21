using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class InvenSkill : MonoBehaviour
{
    public AllSkills allSkills;

    public Card card;

    public Transform cardParentTrm;

    public Button exitBtn;

    public List<Card> deckLists = new List<Card>();
 
    public void EndInven()
    {
        Click.isSelected = false;
        if (Click.clickCard)
        {
            Click.clickCard.SetAlpha(0f);
        }
        Click.clickCard = null;
    }
    private void CreateCard()
    {
        for (int i = 0; i < allSkills._allSkills.Length; i++)
        {
            GameObject cardObj = Instantiate(card, Vector3.zero, Quaternion.identity).gameObject;
            cardObj.GetComponent<Card>().Skill = allSkills._allSkills[i];
            cardObj.transform.SetParent(cardParentTrm);
        }
    }
    private void DeleteCard()
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateCard();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            DeleteCard();
        }
    }
}
