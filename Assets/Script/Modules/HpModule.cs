using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    public Image _effectBar;
    public TextMeshProUGUI text;
    public GameObject _hpbar;

    [Space]
    [Header("Á×À»¶§ ½ÇÇà µÉ ÇÔ¼ö")]
    public UnityEvent deadEvent;

    private Animator _animator;
    [SerializeField] private float effectSpeed = 0.005f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _bar.fillAmount = 1;
        UpdateHPText();
    }

    private void Update()
    {
        if(_bar.fillAmount < _effectBar.fillAmount)
        {
            _effectBar.fillAmount -= effectSpeed;
        }
        else
        {
            _effectBar.fillAmount = _bar.fillAmount;
        }
    }

    public void GetHit(int dmg)
    {
        hp -= dmg;
        UpdateHPText();
        if (hp <= 0)
        {
            Dead();
        }

        _bar.fillAmount = (float)hp / maxHp;
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
        deadEvent.Invoke();
        Debug.Log("À¸¾Ó Á×À½!");
    }

    public void InitHP(int _hp, int _max)
    {
        hp = _hp;
        maxHp = _max;
    }

    public void GetDamaged()
    {
        _animator.SetTrigger("GetHit");
    }
}
