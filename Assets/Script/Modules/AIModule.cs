using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIModule : MonoBehaviour
{
    private MainModule player;

    private EnemyData enemyData;
    private HpModule hpmodule;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<MainModule>();

        enemyData = GetComponent<EnemyData>();
        hpmodule = GetComponent<HpModule>();
        

        Vector3 playerPos = player.transform.position;
        playerPos.y = 0;

        transform.LookAt(playerPos);
    }

    private void Start()
    {
        hpmodule.InitHP(enemyData.Hp, enemyData.Hp);
    }
}
