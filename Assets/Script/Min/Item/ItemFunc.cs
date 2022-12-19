using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunc : MonoBehaviour
{
    public static void HPPotion(int dmg)
    {
        HpModule hp = GameObject.Find("Player").GetComponent<HpModule>();
        hp.GetHp(dmg);
        Debug.Log("체력포션" + dmg);
    }

    public static void DefendPotion(int dmg)
    {
        HpModule hp = GameObject.Find("Player").GetComponent<HpModule>();
        hp.OnShield(dmg);
        Debug.Log("방어도포션" + dmg);

    }
    public static void DamagePotion(int dmg)
    {
        BattleManager battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        for (int i = 0; i < battleManager.fieldEnemies.Count; i++)
        {
            HpModule hp = battleManager.fieldEnemies[i].GetComponent<HpModule>();
            hp.GetHit(dmg, Color.red);
        }
    }

    public static void AgilityPotion(int dmg)
    {

        //민첩 2를 얻습니다
    }
    public static void EnergyPotion(int dmg)
    {
        BattleManager bm = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        int a = bm._battleUI.cost;
        bm._battleUI.CreateCost(a + dmg);
        
        //에너지 2를 얻습니다
    }
    public static void PowerPotion(int dmg)
    {
        //힘 2를 얻습니다
    }
}
