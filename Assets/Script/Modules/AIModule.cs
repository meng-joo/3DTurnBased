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

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<MainModule>();
        _battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        enemyData = GetComponent<EnemyData>();
        hpmodule = GetComponent<HpModule>();
        _enemyAISkill = GetComponent<EnemyAISkill>();

        Vector3 playerPos = player.transform.position;
        playerPos.y = 0;

        transform.LookAt(playerPos);
    }

    private void Start()
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
        gameObject.SetActive(false);
        if (_battleManager.fieldEnemies.Count == 0)
            _battleManager.EndBattle("Win");
    }

    public void WhatToDo()
    {
        
        _enemyAISkill.StartCoroutine("AttackPlayer");
    }
}
