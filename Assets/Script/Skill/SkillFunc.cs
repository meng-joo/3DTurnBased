using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SkillFunc : MonoBehaviour
{
    #region 공격카드
    public static void HitEnemy(GameObject _enemy, int dmg, Color32 _color)
    {
        HpModule hp = _enemy.GetComponent<HpModule>();
        MainModule player = GameObject.Find("Player").GetComponent<MainModule>();
        if(player._HpModule.fear >= 1)
        {
            dmg = (int)(dmg / 0.7f);
        }
        hp.GetHit(dmg, _color);
        //여기서 적때리는 기능 구현해야합
    }

    public static void HitEnemyDamageValue(GameObject _enemy, int attackMultiplier, Color32 _color)
    {
        MainModule player = GameObject.Find("Player").GetComponent<MainModule>();
        float multiplier = attackMultiplier / 100f;
        int dmg = (int)(player.playerDataSO.Ad * multiplier);

        HpModule hp = _enemy.GetComponent<HpModule>();
        hp.GetHit(dmg, _color);
    }

    public static void Teleport(GameObject _enemy, int dmg, Color32 _color)
    {
        GameObject player = GameObject.Find("Player");//.transform.Find("mixamorig:Hips").gameObject;
        SkinnedMeshAfterImage skinnedMeshAfterImage = player.GetComponent<SkinnedMeshAfterImage>();
        skinnedMeshAfterImage.enabled = true;
        GameObject target = player.transform.Find("Target").gameObject;


        Sequence seq = DOTween.Sequence();

        Vector3 originpos = player.transform.localPosition;
        target.transform.SetParent(null);
        seq.Append(player.transform.DOLocalMove(_enemy.transform.position + _enemy.transform.forward * -2, 0.74f));

        seq.AppendCallback(() =>
        {
            skinnedMeshAfterImage.enabled = false;
            player.transform.localPosition = originpos;
            target.transform.SetParent(player.transform);
        });
    }

    public static void AttackAll(GameObject _enemy, int dmg, Color32 _color)
    {
        BattleManager battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        for(int i = 0; i < battleManager.fieldEnemies.Count; i++)
        {
            battleManager.fieldEnemies[i].GetComponent<HpModule>().GetHit(dmg, _color);
        }
    }

    #endregion

    #region 이로운 카드

    public static void AddShield(GameObject _player, int shieldValue, Color32 _color)
    {
        _player.GetComponent<HpModule>().OnShield(shieldValue);
    }
    public static void AddCost(GameObject _player, int value, Color32 _color)
    {
        BattleManager bm = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        int a = bm._battleUI.cost;
        bm._battleUI.CreateCost(a + value);

        bm._battleUI.cost = a + value;
    }

    #endregion

    public static void Weeker(GameObject _enemy, int weekValue, Color32 _color)
    {
        //if (_enemy.GetComponent<HpModule>().weekness < 1)
        //{
        //    //GameObject a = PoolManager.Instance.Pop(PoolType.Status_Week).gameObject;
        //    //RectTransform rectA = a.GetComponent<RectTransform>();
        //    //a.transform.SetParent(_enemy.GetComponent<HpModule>().statusImage.transform);
        //    //Vector3 vec3 = rectA.anchoredPosition;
        //    //vec3.z = 0;
        //    ////rectA.
        //    //rectA.anchoredPosition = vec3;

        //    Debug.Log(a.transform.position);
        //    Debug.Log(a.transform.localPosition);
        //    //a.transform.rotation = Quaternion.Euler(0, 0, 0);
        //    Debug.Log(a.transform.rotation);
        //}
        _enemy.GetComponent<HpModule>().weekness += weekValue;
        //_enemy.GetComponent<HpModule>().AddStatus("Week");
    }

    public static void Fear(GameObject _enemy, int fearValue, Color32 _color)
    {
        if (_enemy.GetComponent<HpModule>().fear < 1)
        {
            GameObject a = PoolManager.Instance.Pop(PoolType.Status_Fear).gameObject;
            a.transform.SetParent(_enemy.GetComponent<HpModule>().statusImage.transform);
            a.transform.position = Vector3.zero;
            Debug.Log(a.transform.position);
            a.transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log(a.transform.rotation);
        }
        _enemy.GetComponent<HpModule>().fear += fearValue;
        //_enemy.GetComponent<HpModule>().AddStatus("Fear");
    }

}

// 클래스 설계도
// 완성본 
