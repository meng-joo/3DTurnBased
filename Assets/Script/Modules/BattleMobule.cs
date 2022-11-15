using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleMobule : MonoBehaviour
{
    MainModule mainModule;

    [Header("���� �ɷ����� ����Ǵ� �Լ�")]
    public UnityEvent _setBattle;

    private void Start()
    {
        mainModule = GetComponent<MainModule>();
    }

    public void StartBattle()
    {
        mainModule.canMove = true;
        mainModule._UIModule.OnInteractionKeyImage(true, "", "!");
        _setBattle.Invoke();
    }
}
