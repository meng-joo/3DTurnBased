using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StateUIManger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI hpText;

    public PlayerDataSO playerDataSO;

    public MainModule mainModule;
    private void Start()
    {
        SetStateTxt();
    }

    void SetStateTxt()
    {
        atkText.text = playerDataSO.Ad.ToString();
        hpText.text = $"{mainModule._HpModule.hp.ToString() + "/" + playerDataSO.Health.ToString()}";
    }
}
