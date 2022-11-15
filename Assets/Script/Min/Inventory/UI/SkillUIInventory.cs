using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SkillUIInventory : SkillUI
{

    public GameObject skillSlot = null;

    public AllSkills allSkills;


    [SerializeField]
    protected Vector2 start;

    [SerializeField]
    protected Vector2 size;

    [SerializeField]
    protected Vector2 space;

    [SerializeField]
    protected int numCols = 4;
    public override void createUISlots()
    {
        //uiSlotLists = new Dictionary<GameObject, InvenSlot>();
        //for (int i = 0; i < inventoryObj.invenSlots.Length; i++)
        //{
        //    GameObject gameObj = staticSlots[i];

        //    AddEventAction(gameObj, EventTriggerType.PointerEnter, delegate { OnEnterSlots(gameObj); });
        //    AddEventAction(gameObj, EventTriggerType.PointerExit, delegate { OnExitSlots(gameObj); });
        //    AddEventAction(gameObj, EventTriggerType.BeginDrag, delegate { OnStartDrag(gameObj); });
        //    AddEventAction(gameObj, EventTriggerType.EndDrag, delegate { OnEndDrag(gameObj); });
        //    AddEventAction(gameObj, EventTriggerType.Drag, delegate { OnMovingDrag(gameObj); });

        //    inventoryObj.invenSlots[i].slotUI = gameObj;
        //    uiSlotLists.Add(gameObj, inventoryObj.invenSlots[i]);
        //}
    }
    public void CreateUISlot()
    {
        for (int i = 0; i < allSkills._allSkills.Length; i++)
        { 
            skillObj.skillInventories.Add(new SkillInventorySlot());
        }

        uiSlotLists = new Dictionary<GameObject, SkillInventorySlot>();

        GameObject gameObj = Instantiate(skillSlot, Vector3.zero, Quaternion.identity, transform);
        gameObj.GetComponent<RectTransform>().anchoredPosition = CalculatePosition(skillObj.skillInventories.Count - 1);

        AddEventAction(gameObj, EventTriggerType.PointerEnter, delegate { OnEnterSlots(gameObj); });
        AddEventAction(gameObj, EventTriggerType.PointerExit, delegate { OnExitSlots(gameObj); });
        AddEventAction(gameObj, EventTriggerType.BeginDrag, delegate { OnStartDrag(gameObj); });
        AddEventAction(gameObj, EventTriggerType.EndDrag, delegate { OnEndDrag(gameObj); });
        AddEventAction(gameObj, EventTriggerType.Drag, delegate { OnMovingDrag(gameObj); });
        AddEventAction(gameObj, EventTriggerType.PointerClick, (data) => { OnClick(gameObj, (PointerEventData)data); });

        skillObj.skillInventories[skillObj.skillInventories.Count -1 ].slotUI = gameObj;
        uiSlotLists.Add(gameObj, skillObj.skillInventories[skillObj.skillInventories.Count -1]);
        gameObj.name += ": " + skillObj.skillInventories[skillObj.skillInventories.Count - 1];
    }
    public Vector3 CalculatePosition(int i)
    {
        float x = start.x + ((space.x + size.x) * (i % numCols));
        float y = start.y + (-(space.y + size.y) * (i / numCols));

        return new Vector3(x, y, 0f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateUISlot();
        }
    }
}
