using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicFunc : MonoBehaviour
{
    public static void ClockClasp()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isClockClasp = true;
        Debug.Log("�� ����� �տ� �ִ� ī�� �� ��� 1�� ���� ��´�");
    }

    public static void LetterOpener()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isLetterOpener = true;
        Debug.Log("ī�带 3�� ����Ҷ����� �� ��ü���� 5�� �������� �ݴϴ�");
    }
    public static void Pantograph()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isPantograph = true;

        Debug.Log("������ ���� 25ȸ��");
    }
    public static void BagofMarbles()
    {

        Debug.Log("���� ���� �� ������ ��ȭ�� �Ǵ�.");
    }
    public static void BloodVial()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isBloodVial = true;

        Debug.Log("���� ���� �� ü���� 2 ȸ���մϴ�");
    }
    public static void Lantern()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isLantern = true;

        Debug.Log("�������� �߰��� ��´�");
    }
    public static void Strawebrry()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isStrawberry = true;

        Debug.Log("Ǯ��ȸ���� �ִ�ü�� + 7");
    }
    
   
    public static void Anchor()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isAnchor = true;

        Debug.Log("���� ���۽� ����  10 ����ϴ�");
    }
    public static void BuringBlood()
    {
        MainModule main = GameObject.Find("Player").GetComponent<MainModule>();
        main.playerDataSO.isBuringBlood = true;
        Debug.Log("���� ����� ü�� 6�� ȸ���Ѵ�.");
    }
}
