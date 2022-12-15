using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicFunc : MonoBehaviour
{
    public static void Ring()
    {
        Debug.Log("카드를 2장 더0");
        //카드를 2장 더 뽑을수있게해줘요
    }

    public static void Blood()
    {
        Debug.Log("최대 체력 6증가");
    }
    public static void Bottle()
    {
        Debug.Log("카드를 10장사용할때마다 1장뽑음");
    }
}
