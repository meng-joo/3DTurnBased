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

    [Header("��Ʋ UI")]
    public BattleUI _battleUI;

    [Space]
    [Header("��UI")]
    public TextMeshProUGUI shieldUI;

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

    [Space]
    [Header("���� ��")]
    public int shield;

    private Animator _animator;
    [SerializeField] private float effectSpeed = 0.005f;

    //private Color textColor = Color.white;

    private void Start()
    {
        shield = 0;
        _animator = GetComponent<Animator>();
        _bar.fillAmount = 1;
        UpdateHPText();

        _battleUI = FindObjectOfType<BattleUI>();
    }

    private void Update()
    {
        shieldUI.text = shield.ToString();

        if (_bar.fillAmount < _effectBar.fillAmount)
        {
            _effectBar.fillAmount -= effectSpeed;
        }
        else
        {
            _effectBar.fillAmount = _bar.fillAmount;
        }
    }

    public void GetHit(int dmg, Color32 _color)
    {
        dmg = Mathf.Max(dmg - shield, 0);
        shield = Mathf.Max(shield - dmg, 0);

        hp = Mathf.Max(0, hp - dmg);
        attackedEvent?.Invoke(dmg);

        if (dmg != 0) _battleUI.SpawnSkillEffectText(dmg.ToString(), _color, transform.position + new Vector3(0.5f, 1, -0.5f));
        else _battleUI.SpawnSkillEffectText("�����", Color.white, transform.position + new Vector3(0.5f, 1, -0.5f));
        
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

    public void OnShield(int value)
    {
        shield += value;
        
    }
}
