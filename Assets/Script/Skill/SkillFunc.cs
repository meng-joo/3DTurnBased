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
        //���⼭ �������� ��� �����ؾ���
    }
}

// Ŭ���� ���赵
// �ϼ��� 
