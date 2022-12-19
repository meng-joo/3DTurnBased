using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicFunc : MonoBehaviour
{
    public static void ClockClasp()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isClockClasp = true;
        Debug.Log("턴 종료시 손에 있는 카드 한 장당 1의 방어도를 얻는다");
    }

    public static void LetterOpener()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isLetterOpener = true;
        Debug.Log("카드를 3장 사용할때마다 적 전체에게 5의 데미지를 줍니다");
    }
    public static void Pantograph()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isPantograph = true;

        Debug.Log("보스에 들어갈때 25회복");
    }
    public static void BagofMarbles()
    {

        Debug.Log("전투 시작 시 적에게 약화를 건다.");
    }
    public static void BloodVial()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isBloodVial = true;

        Debug.Log("전투 시작 시 체력을 2 회복합니다");
    }
    public static void Lantern()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isLantern = true;

        Debug.Log("에너지를 추가로 얻는다");
    }
    public static void Strawebrry()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isStrawberry = true;

        Debug.Log("풀피회복과 최대체력 + 7");
    }
    
   
    public static void Anchor()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isAnchor = true;

        Debug.Log("전투 시작시 방어도를  10 얻습니다");
    }
    public static void BuringBlood()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isBuringBlood = true;
        Debug.Log("전투 종료시 체력 6을 회복한다.");
    }
}
