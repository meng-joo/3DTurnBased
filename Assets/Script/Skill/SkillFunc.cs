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
        //���⼭ �������� ��� �����ؾ���
    }
}

// Ŭ���� ���赵
// �ϼ��� 
