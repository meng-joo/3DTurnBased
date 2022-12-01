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

    public IEnumerator AttackPlayer()
    {
        _aIModule._animator.Play("Attack01");

        yield return new WaitForSeconds(0.4f);
        _aIModule.player._HpModule.GetHit(_aIModule.enemyData.Atk);
        _aIModule.player._HpModule.GetDamaged();
    }
}
