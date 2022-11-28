using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIModule : MonoBehaviour
{
    public MainModule player;

    public EnemyData enemyData;
    private HpModule hpmodule;

    private EnemyAISkill _enemyAISkill;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<MainModule>();

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

    public void WhatToDo()
    {
        _enemyAISkill.AttackPlayer();
    }
}
