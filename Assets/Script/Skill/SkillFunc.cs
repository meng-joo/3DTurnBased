using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SkillFunc : MonoBehaviour
{
    public static void HitEnemy(GameObject _enemy, int dmg)
    {
        _enemy.GetComponent<HpModule>().GetHit(dmg);
        _enemy.transform.DOShakePosition(0.6f, 0.4f, 70);
        //여기서 적때리는 기능 구현해야합
    }
}

// 클래스 설계도
// 완성본 
