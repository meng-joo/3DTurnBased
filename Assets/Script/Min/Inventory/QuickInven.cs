using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class QuickInven : MonoBehaviour
{
    public List<MethodInfo> skillEffect = new List<MethodInfo>();

    public string target;

    public int[] value;

    public ItemObj itemobj;

    public InventoryObj quickInven;

    void Start()
    {
    }
    public void SetItemSkill(ItemObj itemobj, int a)
    {
        SetInven();

        skillEffect = itemobj._SetSkill._Methods;
    }
    public void SetInven()
    {
     //   skillEffect = null;
    }
    public void ClickBtn(int A)
    {
        Debug.Log(quickInven.invenSlots[A].item.item_name);
        int count = 0;
        Debug.Log("ASSA");
        Debug.Log(skillEffect);
        foreach (var method in skillEffect)
        {
            method.Invoke(null, null);
            Debug.Log("¾Ë¹Ùºñ");

            count++;
        }
    }
}
