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
    public Image selectImage;
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
            //gameObject.GetComponentInChildren<TextMeshProUGUI>().text = (skill != null) ? skill.skillInfo._skillName : string.Empty;

            gameObject.transform.Find("NameBackground").GetComponentInChildren<TextMeshProUGUI>().text = (skill != null) ? skill.skillInfo._skillName : string.Empty;
            //gameObject.transform.Find("CostTxt").GetComponent<TextMeshProUGUI>().text = (skill != null) ? skill.skillInfo._skillCost.ToString() : string.Empty;
            gameObject.transform.Find("CostImage").GetComponentInChildren<TextMeshProUGUI>().text = (skill != null) ? skill.skillInfo._skillCost.ToString(): string.Empty;
            SetAlpha(0f);

            if (!isStatic && value == null)
            {
                Destroy(this.gameObject);
            }
        }
    }


    private void Awake()
    {
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
        SetAlpha(1f);
        
        if (Click.isSelected)
        {
            #region 데이터 스왑 
            Skill skillTemp = this.Skill;
            this.Skill = Click.clickCard.Skill;
            Click.clickCard.Skill = skillTemp;
            #endregion

            #region 덱 SO 넣어주기
            //for (int i = 0; i < invenSkill.skillCards.Length; i++)
            //{
            //    invenSkill.skillDeckInvenObj.cards[i] = invenSkill.skillCards[i].Skill;
            //}
            for (int i = 0; i < invenSkill.PlayerDataSO._skills.Length; i++)
            {
                invenSkill.PlayerDataSO._skills[i] = invenSkill.skillCards[i].Skill;
            }
            #endregion

            #region 내가 가지고 있는 스킬 인벤 설정
            invenSkill.tempSkill.RemoveAll(x => x as Skill);

            for (int i = 0; i < invenSkill.contentTrm.childCount; i++)
            {
                invenSkill.tempSkill.Add(invenSkill.contentTrm.GetComponentsInChildren<Card>()[i].Skill);
                invenSkill.skillInvenObj.cards[i] = invenSkill.tempSkill[i];
            }
            #endregion
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
    }

    public override void Init_Push()
    {
    }
}
