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

    public void AttackPlayer()
    {
        _aIModule.player._HpModule.GetDamaged();
        _aIModule.player._HpModule.GetHit(_aIModule.enemyData.Atk);
    }
}
