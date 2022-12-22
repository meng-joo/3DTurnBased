﻿using System.Collections.Generic;
using System.Reflection;
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

    //public static PlayerDataSO PlayerData; //;;
    public PlayerDataSO PlayerData; //;;

    private bool isMinus = false;

    public RectTransform explainTap;

    public GameObject useTap;

    public List<MethodInfo> skillEffect = new List<MethodInfo>();
    public int value;

    public GameObject useOffBtn;

    public AudioClip audioClip;

    public MainModule mainModule;

    public TextMeshProUGUI hp_NomalCam;
    public TextMeshProUGUI hp_Inven;
    public TextMeshProUGUI hp_Mini;

    [Space]
    public TextMeshProUGUI atk_Inven;
    public TextMeshProUGUI atk_NomalCam;

    [Space]
    public TextMeshProUGUI def_Inven;
    public TextMeshProUGUI def_NomalCam;

    [Space]
    public TextMeshProUGUI speed_Inven;
    public TextMeshProUGUI speed_NomalCam;
    private void Awake()
    {
        createUISlots();

        for (int i = 0; i < inventoryObj.invenSlots.Count; i++)
        {
            inventoryObj.invenSlots[i].inventoryObj = inventoryObj;
            inventoryObj.invenSlots[i].OnPostUpload += OnEquipUpdate;
        }

        //PlayerData = AddressableManager.Instance.GetResource<PlayerDataSO>("Assets/SO/Player/PlayerDataSO.asset");
        
        AddEventAction(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInventory(gameObject); });
        AddEventAction(gameObject, EventTriggerType.PointerExit, delegate { OnExitInventory(gameObject); });

        mainModule = GameObject.Find("Player").GetComponent<MainModule>();
    }

    protected virtual void Start()
    {
        for (int i = 0; i < inventoryObj.invenSlots.Count; ++i)
        {
            inventoryObj.invenSlots[i].uploadSlot(inventoryObj.invenSlots[i].item, inventoryObj.invenSlots[i].itemCnt);
        }


        hp_NomalCam.text = $"{mainModule._HpModule.hp} / {PlayerData.Health}"; //._HpModule.maxHp}";
        hp_Inven.text = $"HP : {mainModule._HpModule.hp} /  {PlayerData.Health}"; //{ mainModule._HpModule.maxHp}";
        hp_Mini.text = $"{mainModule._HpModule.hp} / {PlayerData.Health}"; //{ mainModule._HpModule.maxHp}";
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
        inventSlot.slotUI.transform.Find("Image").GetChild(0).GetComponent<Image>().sprite = inventSlot.item.item_id < 0 ? null : inventSlot.ItemObject.itemIcon;
        inventSlot.slotUI.transform.Find("Image").GetChild(0).GetComponent<Image>().color = inventSlot.item.item_id < 0 ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
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

        if (uiSlotLists[gameObj].ItemObject != null)
        {
            //Ray a = Camera.main.ScreenPointToRay(Input.mousePosition);
            //(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            //explainTap.transform.position = Input.mousePosition;

            AudioManager.PlayAudio(UISoundManager.Instance.data.foucusClip);

            if (uiSlotLists[gameObj].inventoryObj.type == InterfaceType.QuickSlot)
            {
                explainTap.localPosition = new Vector3(-610f, -340f, 0f);
                explainTap.GetComponent<RelicInfoImage>().enabled = false;
            }
            else
            {
                explainTap.GetComponent<RelicInfoImage>().enabled = true;
            }

            explainTap.gameObject.SetActive(true);
            explainTap.transform.Find("NameTxt").GetComponent<TextMeshProUGUI>().text = uiSlotLists[gameObj].ItemObject.itemData.item_name;
            explainTap.transform.Find("ExplainTxt").GetComponent<TextMeshProUGUI>().text = uiSlotLists[gameObj].ItemObject.itemSummery;
        }
    }

    public void OnExitSlots(GameObject gameObj)
    {
        MouseTransformData.mouseSlot = null;

        if (uiSlotLists[gameObj].ItemObject != null)
        {
            explainTap.gameObject.SetActive(false);
        }

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

        else
        {
            return;
        }
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
            isMinus = false;
            //주석친 이유는 빈칸에가면 사라지는게 아니라 원래자리로가야하기때문에
            //uiSlotLists[gameObj].destoryItem();
        }
        else if (MouseTransformData.mouseSlot)
        {
            InvenSlot mouseHoverSlotData = MouseTransformData.mouseInventory.uiSlotLists[MouseTransformData.mouseSlot]; //드래그 하고 있는애
            InvenSlot uIInventory = uiSlotLists[gameObj];

            InvenSlot currentInven = isMinus ? uIInventory : mouseHoverSlotData;
            InvenSlot targetInven = isMinus ? mouseHoverSlotData : uIInventory;

            if (inventoryObj.SwapItems(uiSlotLists[gameObj], mouseHoverSlotData) == false)
            {
                isMinus = false;
                return;
            }
            //내가 지금 잡은애가 장비가 아니고 도착할애도 장비가 아니야 그러면 아바가없는거지
            if (currentInven.inventoryObj.type !=  InterfaceType.Equipment && targetInven.inventoryObj.type != InterfaceType.Equipment)
            {
                isMinus = false;
                return;
            }
            else
            {
                if (currentInven.item.abilities != null && !isMinus)
                {
                    foreach (ItemAbility a in currentInven.item.abilities)
                    {
                        Debug.Log($"{a.characterStack} +{a.valStack}");

                        switch (a.characterStack)
                        {
                            case CharacterStack.Str:

                                PlayerData.Ad += a.valStack;

                                atk_Inven.text = $"ATK : {PlayerData.Ad}";
                                atk_NomalCam.text = $"{PlayerData.Ad}";

                                Debug.Log(PlayerData.Ad);
                                break;
                            case CharacterStack.Hp:
                                PlayerData.Health += a.valStack;

                                hp_NomalCam.text = $"{mainModule._HpModule.hp} / {PlayerData.Health}"; //._HpModule.maxHp}";
                                hp_Inven.text = $"HP : {mainModule._HpModule.hp} /  {PlayerData.Health}"; //{ mainModule._HpModule.maxHp}";
                                hp_Mini.text = $"{mainModule._HpModule.hp} / {PlayerData.Health}"; //{ mainModule._HpModule.maxHp}";
                                Debug.Log(mainModule._HpModule.hp);
                                Debug.Log(mainModule._HpModule.maxHp);

                                Debug.Log(PlayerData.Health);
                                break;
                            case CharacterStack.Speed:
                                PlayerData.Speed += a.valStack;

                                speed_Inven.text = $"SPEED : {PlayerData.Speed}";
                                speed_NomalCam.text = $"{PlayerData.Speed}";

                                Debug.Log(PlayerData.Speed);
                                break;
                            case CharacterStack.Defend:
                                PlayerData.Def += a.valStack;

                                def_Inven.text = $"DEF : {PlayerData.Def}";
                                def_NomalCam.text = $"{PlayerData.Def}";

                                Debug.Log(PlayerData.Def);
                                break;
                        }

                    }
                }

                if (targetInven.item.abilities != null)
                {
                    foreach (ItemAbility a in targetInven.item.abilities)
                    {
                        switch (a.characterStack)
                        {
                            case CharacterStack.Str:
                                PlayerData.Ad -= a.valStack;


                                atk_Inven.text = $"ATK : {PlayerData.Ad}";
                                atk_NomalCam.text = $"{PlayerData.Ad}";
                                //ad = a.valStack;
                                break;
                            case CharacterStack.Hp:
                                PlayerData.Health -= a.valStack;

                                hp_NomalCam.text = $"{mainModule._HpModule.hp} / {PlayerData.Health}"; //._HpModule.maxHp}";
                                hp_Inven.text = $"HP : {mainModule._HpModule.hp} /  {PlayerData.Health}"; //{ mainModule._HpModule.maxHp}";
                                hp_Mini.text = $"{mainModule._HpModule.hp} / {PlayerData.Health}"; //{ mainModule._HpModule.maxHp}";


                                break;
                            case CharacterStack.Speed:
                                PlayerData.Speed -= a.valStack;

                                speed_Inven.text = $"SPEED : {PlayerData.Speed}";
                                speed_NomalCam.text = $"{PlayerData.Speed}";

                                break;
                            case CharacterStack.Defend:
                                PlayerData.Def -= a.valStack;

                                def_Inven.text = $"DEF : {PlayerData.Def}";
                                def_NomalCam.text = $"{PlayerData.Def}";

                                break;
                        }
                    }
                }
                isMinus = false;
            }
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

        if (uiSlotLists[gameObj].item.item_id < 0)
        {
            return;
        }

        //  if (slot.ItemObject.itemType == ItemType.Default)
        if (inventoryObj.type == InterfaceType.QuickSlot)
        {


            Debug.Log("사용아이템");
            useOffBtn.SetActive(true);
            useTap.gameObject.SetActive(true);

            //useTap.transform.position = pointerEventdata.position;
            //useTap.transform.position += new Vector3(0f, 200f, 0f);

            useTap.transform.Find("UseBtn").GetComponent<Button>().onClick.RemoveAllListeners();

            useTap.transform.Find("UseBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                skillEffect = slot.ItemObject._SetItem._Methods;

                int value = slot.ItemObject.value[0];
                //선한쌤한테 할진물들
                // 밸류가 중첩이됨

                // 아이템이 배틀에서만 사용할수 있고 전투중이아니면
                if (mainModule.isBattle == false && slot.ItemObject.itemData.inBattle == true)
                {
                    DialogManager.Instance.ShowText("전투중에만 사용할수 있는 아이템입니다");
                    return;
                }

                foreach (var method in skillEffect)
                {
                    method.Invoke(null, new object[] { value });
                }
                    //_battleUI.SpawnSkillEffectText(value[count].ToString(), skillText, transform.position);

                //밑의 줄이 복수 딴것도잘되
                slot.uploadSlot(slot.ItemObject.itemData, --slot.itemCnt);
                useTap.gameObject.SetActive(false);
                useOffBtn.gameObject.SetActive(false);
            });
            useTap.transform.Find("RemoveBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                slot.uploadSlot(null, 0);
                useTap.gameObject.SetActive(false);
                useOffBtn.gameObject.SetActive(false);
            });
        }
        else
        {
            useTap.transform.Find("UseBtn").GetComponent<Button>().onClick.RemoveAllListeners();
            useTap.transform.Find("RemoveBtn").GetComponent<Button>().onClick.RemoveAllListeners();
            useTap.gameObject.SetActive(false);
        }
    }

    protected virtual void OnRightClick(InvenSlot invenSlot)
    {
    }

    protected virtual void OnLeftClick(InvenSlot invenSlot)
    {
    }
}