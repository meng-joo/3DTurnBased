using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIModule : MonoBehaviour
{
    public MainModule player;
    public Animator _animator;

    public string[] skillName = new string[0];
    
    private BattleManager _battleManager;
    public EnemyData enemyData;
    public HpModule hpmodule;

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

    private void LateUpdate()
    {
        Vector3 dir = target.transform.position;
        dir.y = 0;
        transform.LookAt(dir);
        //Quaternion a = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
        //transform.rotation = ;// = a;
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
        _battleManager.fieldEnemies.Remove(gameObject);
        if (_battleManager.fieldEnemies.Count == 0)
        {
            Time.timeScale = 0.3f;
            yield return new WaitForSecondsRealtime(2.5f);
            Time.timeScale = 1;
        }
        _animator.Play("Die");

        yield return new WaitForSeconds(0.8f);
        transform.DOScale(0, 0.5f);

        if (enemyType >= PoolType.Boss1) {
            player.playerDataSO.canBattle = false;
            player.playerDataSO.killEnemy = 0;
            player.canRelic = true;
            player.playerDataSO.stage++;

            player.stageClear = true;;
        }
        yield return new WaitForSeconds(.5f);

        //gameObject.SetActive(false);
        PoolManager.Instance.Push(enemyType, gameObject);



        if (_battleManager.fieldEnemies.Count == 0)
        {
            if (enemyType >= PoolType.Boss1)
            {
                _battleManager.EndBattle("Win", true);
            }
            else
            {
                _battleManager.EndBattle("Win", false);
            }
        }
    }

    public void WhatToDo()
    {
        int a = Random.Range(0, skillName.Length);

        if (hpmodule.hp < hpmodule.maxHp * 0.7f && skillName[a] == "Heal")
        {
            a = Random.Range(0, skillName.Length -1);
        }

        _enemyAISkill.StartCoroutine(skillName[a]);
    }
}
