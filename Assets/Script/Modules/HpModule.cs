using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

[System.Serializable]
public class HitEvent : UnityEvent<int>
{
}

public class HpModule : MonoBehaviour
{
    [Header("�� ��")]
    public int maxHp;

    [Space]
    [Header("���� ��")]
    public int hp;

    [Space]
    [Header("UI��")]
    public Image _bar;
    public Image _effectBar;
    public TextMeshProUGUI text;
    public GameObject _hpbar;

    [Space]
    [Header("������ ���� �� �Լ�")]
    public HitEvent attackedEvent;

    [Space]
    [Header("������ ���� �� �Լ�")]
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
        attackedEvent?.Invoke(dmg);
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
        Debug.Log("���� ����!");
    }

    public void InitHP(int _hp, int _max)
    {
        hp = _hp;
        maxHp = _max;
    }

    public void GetDamaged()
    {
        transform.DOShakePosition(0.34f, 0.4f, 80);
        _animator.SetTrigger("GetHit");
    }
}
