using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleMobule : MonoBehaviour
{
    MainModule mainModule;

    [Header("전투 걸림?")]
    public bool inBattle;

    [Space]
    [Header("전투 걸렸을때 실행되는 함수")]
    public UnityEvent _setBattle;

    private void Start()
    {
        inBattle = false;
        mainModule = GetComponent<MainModule>();
    }

    public void StartBattle()
    {
        mainModule.canMove = inBattle = true;
        
        mainModule._UIModule.OnInteractionKeyImage(true, "", "!");
        _setBattle.Invoke();
    }
}
