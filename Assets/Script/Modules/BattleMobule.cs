using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class BattleMobule : MonoBehaviour
{
    MainModule mainModule;

    [Header("���� �ɸ�?")]
    public bool inBattle;

    [Space]
    [Header("���� �ɷ����� ����Ǵ� �Լ�")]
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

    IEnumerator ShakeBattleCam(float strenght)
    {
        var _multichannel = mainModule.battleCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _multichannel.m_AmplitudeGain = strenght;

        yield return new WaitForSeconds(0.4f);

        _multichannel.m_AmplitudeGain = 0;
    }
}
