using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIModule : MonoBehaviour
{
    public MainModule player;
    public Animator _animator;

    private BattleManager _battleManager;
    public EnemyData enemyData;
    private HpModule hpmodule;

    private EnemyAISkill _enemyAISkill;
    public PoolType enemyType;
    private GameObject target;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<MainModule>();
        _battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        target = player.transform.Find("Target").gameObject;

        enemyData = GetComponent<EnemyData>();
        hpmodule = GetComponent<HpModule>();
        _enemyAISkill = GetComponent<EnemyAISkill>();
    }

    private void Update()
    {
        transform.LookAt(target.transform.position);
    }

    public void SetEnemy()
    {
        hpmodule.InitHP(enemyData.Hp, enemyData.Hp);
    }

    public void EnemyDead()
    {
        StartCoroutine(EnemyDeadCorou());
    }

    IEnumerator EnemyDeadCorou()
    {
        _animator.Play("Die");

        yield return new WaitForSeconds(1.3f);

        _battleManager.fieldEnemies.Remove(gameObject);
        //gameObject.SetActive(false);
        PoolManager.Instance.Push(enemyType, gameObject);
        
        if (_battleManager.fieldEnemies.Count == 0)
            _battleManager.EndBattle("Win");
    }

    public void WhatToDo()
    {
        _enemyAISkill.StartCoroutine("AttackPlayer");
    }
}
