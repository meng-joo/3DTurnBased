using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAISkill : MonoBehaviour
{
    public AIModule _aIModule;

    private void Start()
    {
        _aIModule = GetComponent<AIModule>();
    }

    public IEnumerator Attack()
    {
        _aIModule._animator.Play("Attack01");

        yield return new WaitForSeconds(0.4f);

        int dmg = (int)Random.Range(_aIModule.enemyData.Atk / 2f, _aIModule.enemyData.Atk);

        _aIModule.player._HpModule.GetHit(dmg, Color.red);
    }

    public IEnumerator Defend()
    {
        _aIModule._animator.Play("Defend");

        int value = Random.Range(_aIModule.enemyData.Def, _aIModule.enemyData.Def * 2);



        yield return new WaitForSeconds(0.4f);
        _aIModule.hpmodule.OnShield(value);
    }

    public IEnumerator Heal()
    {
        _aIModule._animator.Play("Heal");

        int value = Random.Range(_aIModule.enemyData.Hp / 5, _aIModule.enemyData.Hp / 2);

        yield return new WaitForSeconds(0.4f);
        _aIModule.hpmodule.GetHp(value);
    }
}
