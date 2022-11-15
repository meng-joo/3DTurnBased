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

public class Card : MonoBehaviour
{
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
            gameObject.transform.Find("Image").GetComponent<Image>().sprite = (skill != null) ?  skill.skillInfo._skillImage : null; 
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
            Skill skillTemp = this.Skill;
            this.Skill = Click.clickCard.Skill;
            Click.clickCard.Skill = skillTemp;
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


  
    

}
