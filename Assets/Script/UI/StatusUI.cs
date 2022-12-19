using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusUI : MonoBehaviour
{
    public TextMeshProUGUI hp_NomalCam;
    public TextMeshProUGUI hp_Inven;
    public TextMeshProUGUI hp_Mini;

    [Space]
    public TextMeshProUGUI atk_Inven;
    public TextMeshProUGUI atk_NomalCam;
    
    [Space]
    public TextMeshProUGUI def_Inven;
    public TextMeshProUGUI def_NomalCam;

    [Space]
    public TextMeshProUGUI speed_Inven;
    public TextMeshProUGUI speed_NomalCam;

    [Space]
    public MainModule mainModule;

    private void Start()
    {
        StartCoroutine(UpdateText());
    }

    IEnumerator UpdateText()
    {
        while (true)
        {
            hp_NomalCam.text = $"{mainModule._HpModule.hp} / {mainModule._HpModule.maxHp}";
            hp_Inven.text = $"HP : {mainModule._HpModule.hp} / {mainModule._HpModule.maxHp}";
            hp_Mini.text = $"{mainModule._HpModule.hp} / {mainModule._HpModule.maxHp}";

            atk_Inven.text = $"ATK : {mainModule.playerDataSO.Ad}";
            atk_NomalCam.text = $"{mainModule.playerDataSO.Ad}";

            def_Inven.text = $"DEF : {mainModule.playerDataSO.Def}";
            def_NomalCam.text = $"{mainModule.playerDataSO.Def}";

            speed_Inven.text = $"SPEED : {mainModule.playerDataSO.Speed}";
            speed_NomalCam.text = $"{mainModule.playerDataSO.Speed}";
            yield return new WaitForEndOfFrame();
        }
    }
}
