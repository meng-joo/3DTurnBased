using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public static class MouseTransformData
{ 
    public static UIInventory mouseInventory; 
    public static GameObject mouseDragging; 
    public static GameObject mouseSlot;
}
 
[RequireComponent(typeof(EventTrigger))]
public abstract class UIInventory : MonoBehaviour
{
    public InventoryObj inventoryObj; 
    private InventoryObj beforeInventoryObj;
     
    public Dictionary<GameObject, InvenSlot> uiSlotLists = new Dictionary<GameObject, InvenSlot>();

    public static PlayerDataSO PlayerData; //;;

    private bool isMinus = false;

    private void Awake()
    { 
        createUISlots();
         
        for (int i = 0; i < inventoryObj.invenSlots.Count; i++)
        { 
            inventoryObj.invenSlots[i].inventoryObj = inventoryObj; 
            inventoryObj.invenSlots[i].OnPostUpload += OnEquipUpdate;
        }

        PlayerData = AddressableManager.Instance.GetResource<PlayerDataSO>("Assets/SO/Player/PlayerDataSO.asset");

        AddEventAction(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInventory(gameObject); }); 
        AddEventAction(gameObject, EventTriggerType.PointerExit, delegate { OnExitInventory(gameObject); });
    }
     
    protected virtual void Start()
    { 
        for (int i = 0; i < inventoryObj.invenSlots.Count; ++i)
        { 
            inventoryObj.invenSlots[i].uploadSlot(inventoryObj.invenSlots[i].item, inventoryObj.invenSlots[i].itemCnt);
        }
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
     
    public void OnEquipUpdate(InvenSlot inventSlot)
    {
        inventSlot.slotUI.transform.GetChild(0).GetComponent<Image>().sprite = inventSlot.item.item_id < 0 ? null : inventSlot.ItemObject.itemIcon;
        inventSlot.slotUI.transform.GetChild(0).GetComponent<Image>().color = inventSlot.item.item_id < 0 ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
        inventSlot.slotUI.GetComponentInChildren<TextMeshProUGUI>().text = inventSlot.item.item_id < 0 ? string.Empty : (inventSlot.itemCnt == 1 ? string.Empty : inventSlot.itemCnt.ToString("n0"));
    }
     
    public void OnEnterInventory(GameObject gameObj)
    { 
        MouseTransformData.mouseInventory = gameObj.GetComponent<UIInventory>();
    }
     
    public void OnExitInventory(GameObject gameObj)
    {
        MouseTransformData.mouseInventory = null;
    }
     
    public void OnEnterSlots(GameObject gameObj)
    { 
        MouseTransformData.mouseSlot = gameObj;
        MouseTransformData.mouseInventory = gameObj.GetComponentInParent<UIInventory>();
    }

    public void OnExitSlots(GameObject gameObj)
    {
        MouseTransformData.mouseSlot = null;
    } 

    private GameObject AddEventDragImage(GameObject gameObj)
    { 
        if (uiSlotLists[gameObj].item.item_id < 0)
        {
            return null;
        }
         
        GameObject imgDrags = new GameObject();
         
        RectTransform rectTransform = imgDrags.AddComponent<RectTransform>(); 
        rectTransform.sizeDelta = new Vector2(50, 50); 
        imgDrags.transform.SetParent(transform.parent);
         
        Image image = imgDrags.AddComponent<Image>(); 
        image.sprite = uiSlotLists[gameObj].ItemObject.itemIcon; 
        image.raycastTarget = false;
         
        imgDrags.name = "Drag Image";

        return imgDrags;
    }
     
    public void OnStartDrag(GameObject gameObj)
    { 
        MouseTransformData.mouseDragging = AddEventDragImage(gameObj);

        if (MouseTransformData.mouseInventory.uiSlotLists[MouseTransformData.mouseSlot].inventoryObj.type == InterfaceType.Equipment)
            isMinus = true;
    }
     
    public void OnMovingDrag(GameObject gameObj)
    { 
        if (MouseTransformData.mouseDragging == null)
        {
            return;
        } 
        MouseTransformData.mouseDragging.GetComponent<RectTransform>().position = Input.mousePosition;
    }
    
    public void OnEndDrag(GameObject gameObj)
    { 
        Destroy(MouseTransformData.mouseDragging); 
        if (MouseTransformData.mouseInventory == null)
        { 
            //주석친 이유는 빈칸에가면 사라지는게 아니라 원래자리로가야하기때문에
            //uiSlotLists[gameObj].destoryItem();
        }
        else if (MouseTransformData.mouseSlot)
        { 
            InvenSlot mouseHoverSlotData = MouseTransformData.mouseInventory.uiSlotLists[MouseTransformData.mouseSlot]; //드래그 하고 있는애
            //if(uiSlotLists[gameObj].inventoryObj.type == InterfaceType.Equipment)
            Debug.Log(mouseHoverSlotData.item.item_name);

            Debug.Log(uiSlotLists[gameObj].item.item_name);
            ItemAbility[] current = mouseHoverSlotData.item.abilities;

            inventoryObj.SwapItems(uiSlotLists[gameObj], mouseHoverSlotData);
            if (uiSlotLists[gameObj].inventoryObj.type != InterfaceType.Equipment && uiSlotLists[gameObj].item != null)
            {
                foreach (ItemAbility a in current)
                {
                    Debug.Log("과연한민영개병신");
                    switch (a.characterStack)
                    {
                        case CharacterStack.Str:
                            PlayerData.Ad += a.valStack;
                            //ad = a.valStack;
                            break;
                        case CharacterStack.Hp:
                            PlayerData.Health += a.valStack;
                            //hp = mitemAbility[i].valStack;
                            break;
                        case CharacterStack.Benefit_Effect:
                            break;
                        case CharacterStack.Detrimental_Effect:
                            break;
                    }
                }
                isMinus = true;
            Debug.Log(isMinus);
            }
            Debug.Log(uiSlotLists[gameObj].item.abilities);

            if (isMinus)
            {
                    Debug.Log("과연맹주영개병신");
                foreach (ItemAbility a in current)
                {
                    switch (a.characterStack)
                    {
                        case CharacterStack.Str:
                            PlayerData.Ad -= a.valStack;
                            //ad = a.valStack;
                            break;
                        case CharacterStack.Hp:
                            PlayerData.Health -= a.valStack;
                            //hp = mitemAbility[i].valStack;
                            break;
                        case CharacterStack.Benefit_Effect:
                            break;
                        case CharacterStack.Detrimental_Effect:
                            break;
                    }
                }
            }

            isMinus = false;
        }
    }

    public void OnClick(GameObject gameObj, PointerEventData pointerEventdata)
    {
        InvenSlot slot = uiSlotLists[gameObj];
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

    protected virtual void OnRightClick(InvenSlot invenSlot)
    {
    }

    protected virtual void OnLeftClick(InvenSlot invenSlot)
    {
    } 
} 