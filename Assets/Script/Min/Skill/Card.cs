using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;
using TMPro;

public static class Click
{
    public static Card clickCard; //현재클릭한걸알수있어
    public static bool isSelected;
}

public static class ExtensionList
{
    public static void Swap<T>(this List<T> list, int from, int to)
    {
        T tmp = list[from];
        list[from] = list[to];
        list[to] = tmp;
    }
}

public class Card : PoolAbleObject
{
    public bool isFull = false;

    InvenSkill invenSkill;

    public Button cardBtn;
    private Image selectImage;
    [SerializeField] private bool isStatic;
    Skill skill;
    public Skill Skill
    {
        get
        {
            return skill;
        }
        set
        {
            skill = value;
            gameObject.transform.Find("Image").GetComponent<Image>().sprite = (skill != null) ? skill.skillInfo._skillImage : null;
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = (skill != null) ? skill.skillInfo._skillName : string.Empty;
            SetAlpha(0f);

            if (!isStatic && value == null)
            {
                Destroy(this.gameObject);
            }
        }
    }


    private void Awake()
    {
        selectImage = transform.Find("SelectImage").GetComponent<Image>();
        invenSkill = FindObjectOfType<InvenSkill>();
    }
    private void Start()
    {
        AddEventAction(this, EventTriggerType.PointerClick, (data) => { OnClick(this, (PointerEventData)data); });
    }
    public void SelectClick()
    {
        Debug.Log(selectImage.color.a);
    }
    public void SetAlpha(float alpha)
    {
        Color color = selectImage.color;
        color.a = alpha;
        selectImage.color = color;
    }
    public void OnClick(Card card, PointerEventData data)
    {

        //if (isStatic && Click.clickCard.isStatic)
        //{
        //    return;
        //}

        SetAlpha(1f);

        if (Click.isSelected)
        {

            Skill skillTemp = this.Skill;
            this.Skill = Click.clickCard.Skill;
            Click.clickCard.Skill = skillTemp;

            invenSkill.deckLists.RemoveRange(0, invenSkill.skillDeck.childCount);

            for (int i = 0; i < invenSkill.skillDeck.childCount; i++)
            {
                invenSkill.deckLists.Add(invenSkill.skillDeck.GetChild(i).GetComponent<Card>());
            }


            // if (isStatic && !Click.clickCard.isStatic) //내가 스태틱이고 누른것도 스태틱이고
            //{
            //    Debug.Log("맹중영바보");
            //    invenSkill.deckLists.Add(card);
            //    invenSkill.deckLists.Remove(Click.clickCard);
            //}
            //else if (!isStatic && Click.clickCard.isStatic)
            //{
            //    Debug.Log("한민영ㅇ바");
            //    invenSkill.deckLists.Remove(Click.clickCard);
            //    invenSkill.deckLists.Add(card);
            //}
        }
        else
        {
            Click.clickCard = card;
        }

        Click.isSelected = !Click.isSelected;
    }

    protected void AddEventAction(Card card, EventTriggerType eventTriggerType, UnityAction<BaseEventData> BaseEventDataAction)
    {
        EventTrigger eventTrigger = card.GetComponent<EventTrigger>();

        if (!eventTrigger)
        {
            Debug.LogWarning("Nothing Events!");
            return;
        }

        EventTrigger.Entry eventTriggerEntry = new EventTrigger.Entry { eventID = eventTriggerType };
        eventTriggerEntry.callback.AddListener(BaseEventDataAction);
        eventTrigger.triggers.Add(eventTriggerEntry);
    }

    public override void Init_Pop()
    {
        throw new NotImplementedException();
    }

    public override void Init_Push()
    {
        throw new NotImplementedException();
    }
}
