using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillFunc : MonoBehaviour
{

    public void HitEnemy(GameObject _enemy, int _damage)
    {
        _enemy.GetComponent<HpModule>();
        //여기서 적때리는 기능 구현해야합
    }
        

}

// 클래스 설계도
// 완성본 
