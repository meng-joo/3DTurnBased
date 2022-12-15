using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SkillFunc : MonoBehaviour
{
    #region ����ī��
    public static void HitEnemy(GameObject _enemy, int dmg, Color32 _color)
    {
        HpModule hp = _enemy.GetComponent<HpModule>();
        hp.GetHit(dmg, _color);
        hp.GetDamaged();
        //���⼭ �������� ��� �����ؾ���
    }

    public static void HitEnemyDamageValue(GameObject _enemy, int attackMultiplier, Color32 _color)
    {
        MainModule player = GameObject.Find("Player").GetComponent<MainModule>();
        float multiplier = attackMultiplier / 100f;
        int dmg = (int)(player.playerDataSO.Ad * multiplier);

        HpModule hp = _enemy.GetComponent<HpModule>();
        hp.GetHit(dmg, _color);
        hp.GetDamaged();
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
            battleManager.fieldEnemies[i].GetComponent<HpModule>().GetDamaged();
        }
    }

    #endregion

    #region �̷ο� ī��

    public static void AddShield(GameObject _player, int shieldValue, Color32 _color)
    {
        _player.GetComponent<HpModule>().OnShield(shieldValue);
    }

    #endregion

    public static void Weeker(GameObject _enemy, int weekValue, Color32 _color)
    {

    }
}

// Ŭ���� ���赵
// �ϼ��� 
