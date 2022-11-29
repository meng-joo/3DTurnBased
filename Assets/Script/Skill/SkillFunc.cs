using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SkillFunc : MonoBehaviour
{
    public static void HitEnemy(GameObject _enemy, int dmg)
    {
        HpModule hp = _enemy.GetComponent<HpModule>();
        hp.GetHit(dmg);
        hp.GetDamaged();
        _enemy.transform.DOShakePosition(0.34f, 0.4f, 80);
        //여기서 적때리는 기능 구현해야합
    }

    public static void Teleport(GameObject _enemy, int dmg)
    {
        GameObject player = GameObject.Find("Player");//.transform.Find("mixamorig:Hips").gameObject;
        GameObject target = player.transform.Find("Target").gameObject;

        Sequence seq = DOTween.Sequence();

        Vector3 originpos = player.transform.localPosition;
        target.transform.SetParent(null);
        seq.Append(player.transform.DOLocalMove(_enemy.transform.position + _enemy.transform.forward * -2, 0.4f));

        seq.AppendInterval(.6f);

        seq.AppendCallback(() =>
        {
            player.transform.localPosition = originpos;
            target.transform.SetParent(player.transform);
        });
    }
}

// 클래스 설계도
// 완성본 
