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
    public PlayerDataSO player;


    private void Awake()
    {
        hp_NomalCam.text = $"{player.Health} / {player.Health}"; //._HpModule.maxHp}";
        hp_Inven.text = $"HP : {player.Health} /  {player.Health}"; //{ mainModule._HpModule.maxHp}";
        hp_Mini.text = $"{player.Health} / {player.Health}"; //{ mainModule._HpModule.maxHp}";
    }
    private void Start()
    {
        Invoke("A", 1.5f);   

        //hp_NomalCam.text = $"{mainModule.playerDataSO.Health} / {mainModule.playerDataSO.Health}"; //._HpModule.maxHp}";
        //hp_Inven.text = $"HP : {mainModule.playerDataSO.Health} /  {mainModule.playerDataSO.Health}"; //{ mainModule._HpModule.maxHp}";
        //hp_Mini.text = $"{mainModule.playerDataSO.Health} / {mainModule.playerDataSO.Health}"; //{ mainModule._HpModule.maxHp}";


        //atk_Inven.text = $"ATK : {mainModule.playerDataSO.Ad}";
        //atk_NomalCam.text = $"{mainModule.playerDataSO.Ad}";

        //def_Inven.text = $"DEF : {mainModule.playerDataSO.Def}";
        //def_NomalCam.text = $"{mainModule.playerDataSO.Def}";

        //speed_Inven.text = $"SPEED : {mainModule.playerDataSO.Speed}";
        //speed_NomalCam.text = $"{mainModule.playerDataSO.Speed}";
        //   StartCoroutine(UpdateText());
    }
    private void A()
    {
        hp_NomalCam.text = $"{player.Health} / {player.Health}"; //._HpModule.maxHp}";
        hp_Inven.text = $"HP : {player.Health} /  {player.Health}"; //{ mainModule._HpModule.maxHp}";
        hp_Mini.text = $"{player.Health} / {player.Health}"; //{ mainModule._HpModule.maxHp}";
    }
    IEnumerator UpdateText()
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
            yield return new WaitForSeconds(3f);
        
    }
}
