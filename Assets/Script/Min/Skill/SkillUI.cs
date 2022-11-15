using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class SkillMouseTransformData
{
    public static SkillUI mouseInventory;
    public static GameObject mouseDragging;
    public static GameObject mouseSlot;
}

[RequireComponent(typeof(EventTrigger))]
public abstract class SkillUI : MonoBehaviour
{

    public SkillInventoryObj skillObj;


    public Dictionary<GameObject, SkillInventorySlot> uiSlotLists = new Dictionary<GameObject, SkillInventorySlot>();


    private void Awake()
    {
        createUISlots();

        for (int i = 0; i < skillObj.skillInventories.Count; i++)
        {
            skillObj.skillInventories[i].skillInventoryObj = skillObj;
            skillObj.skillInventories[i].OnPostUpload += OnEquipUpdate;
        }

        AddEventAction(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInventory(gameObject); });
        AddEventAction(gameObject, EventTriggerType.PointerExit, delegate { OnExitInventory(gameObject); });
    }

    protected virtual void Start()
    {
        //for (int i = 0; i < inventoryObj.invenSlots.Count; ++i)
        //{
        //    inventoryObj.invenSlots[i].uploadSlot(inventoryObj.invenSlots[i].item, inventoryObj.invenSlots[i].itemCnt);
        //}
    }

    public abstract void createUISlots();

    protected void AddEventAction(GameObject gameObj, EventTriggerType eventTriggerType, UnityAction<BaseEventData> BaseEventDataAction)
    {
        EventTrigger eventTrigger = gameObj.GetComponent<EventTrigger>();

        if (!eventTrigger)
        {
            Debug.LogWarning("Nothing Events!");
            return;
        }

        EventTrigger.Entry eventTriggerEntry = new EventTrigger.Entry { eventID = eventTriggerType };
        eventTriggerEntry.callback.AddListener(BaseEventDataAction);
        eventTrigger.triggers.Add(eventTriggerEntry);
    }

    public void OnEquipUpdate(SkillInventorySlot skillinventSlot)
    {
        skillinventSlot.slotUI.transform.GetChild(0).GetComponent<Image>().sprite = skillinventSlot.SkillObject.allSkills._allSkills[0].skillInfo._skillImage;
        skillinventSlot.slotUI.GetComponentInChildren<TextMeshProUGUI>().text = skillinventSlot.SkillObject.allSkills._allSkills[0].skillInfo._skillName.ToString();
    }

    public void OnEnterInventory(GameObject gameObj)
    {
        SkillMouseTransformData.mouseInventory = gameObj.GetComponent<SkillUI>();
    }

    public void OnExitInventory(GameObject gameObj)
    {
        SkillMouseTransformData.mouseInventory = null;
    }

    public void OnEnterSlots(GameObject gameObj)
    {
        SkillMouseTransformData.mouseSlot = gameObj;
        SkillMouseTransformData.mouseInventory = gameObj.GetComponentInParent<SkillUI>();
    }

    public void OnExitSlots(GameObject gameObj)
    {
        SkillMouseTransformData.mouseSlot = null;
    }

    private GameObject AddEventDragImage(GameObject gameObj)
    {
        GameObject imgDrags = new GameObject();

        RectTransform rectTransform = imgDrags.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(50, 50);
        imgDrags.transform.SetParent(transform.parent);

        Image image = imgDrags.AddComponent<Image>();
        image.sprite = uiSlotLists[gameObj].skillInventoryObj.allSkills._allSkills[0].skillInfo._skillImage;
        image.raycastTarget = false;
        imgDrags.name = "Drag Image";

        return imgDrags;
    }

    public void OnStartDrag(GameObject gameObj)
    {
        SkillMouseTransformData.mouseDragging = AddEventDragImage(gameObj);
    }

    public void OnMovingDrag(GameObject gameObj)
    {
        if (SkillMouseTransformData.mouseDragging == null)
        {
            return;
        }
        SkillMouseTransformData.mouseDragging.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void OnEndDrag(GameObject gameObj)
    {
        Destroy(SkillMouseTransformData.mouseDragging);
        if (SkillMouseTransformData.mouseInventory == null)
        {
        }
        else if (MouseTransformData.mouseSlot)
        {
            SkillInventorySlot skillInventorySlot = SkillMouseTransformData.mouseInventory.uiSlotLists[SkillMouseTransformData.mouseSlot];
        }
    }

    public void OnClick(GameObject gameObj, PointerEventData pointerEventdata)
    {
        SkillInventorySlot slot = uiSlotLists[gameObj];
        if (slot == null)
        {
            return;
        }

        if (pointerEventdata.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick(slot);
        }
        else if (pointerEventdata.button == PointerEventData.InputButton.Right)
        {
            OnRightClick(slot);
        }
    }

    protected virtual void OnRightClick(SkillInventorySlot skillInventorySlot)
    {
    }

    protected virtual void OnLeftClick(SkillInventorySlot skillInventorySlot)
    {
    }
}