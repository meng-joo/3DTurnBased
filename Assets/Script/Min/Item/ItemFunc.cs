using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunc : MonoBehaviour
{
    public static void HPPotion(GameObject player, int value)
    {
        Debug.Log("체력포션");
        //HpModule hp = player.GetComponent<HpModule>();
        //hp.GetHit(value);
    }
    public static void DamagePotion()
    {
        Debug.Log("데미지포션");
    }
}
