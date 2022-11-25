using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HpModule : MonoBehaviour
{
    [Header("ÃÑ ÇÇ")]
    public int maxHp;

    [Space]
    [Header("ÇöÀç ÇÇ")]
    public int hp;

    [Space]
    [Header("UIµé")]
    public Image _bar;
    public TextMeshProUGUI text;
    public GameObject _hpbar;

    private void Start()
    {
        _bar.fillAmount = 1;
        UpdateHPText();
    }

    public void GetHit(int dmg)
    {
        hp -= dmg;
        UpdateHPText();
        if (hp <= 0)
        {
            Dead();
        }

        _bar.fillAmount = hp / maxHp;
    }

    public void UpdateHPText()
    {
        text.text = $"{hp} / {maxHp}";
    }

    public void SetAvtiveHpbar(bool isOn)
    {
        _hpbar.SetActive(isOn);
    }

    public void Dead()
    {
        Debug.Log("À¸¾Ó Á×À½!");
    }

    public void InitHP(int _hp, int _max)
    {
        hp = _hp;
        maxHp = _max;
    }
}
