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
    [Header("총 피")]
    public int maxHp;

    [Header("배틀 UI")]
    public BattleUI _battleUI;

    [Space]
    [Header("방어막UI")]
    public TextMeshProUGUI shieldUI;

    [Space]
    [Header("현재 피")]
    public int hp;

    [Space]
    [Header("UI들")]
    public Image _bar;
    public Image _effectBar;
    public TextMeshProUGUI text;
    public GameObject _hpbar;
    public Image statusImage;

    [Space]
    [Header("맞을때 실행 될 함수")]
    public HitEvent attackedEvent;

    [Space]
    [Header("죽을때 실행 될 함수")]
    public UnityEvent deadEvent;

    [Space]
    [Header("현재 방어도")]
    public int shield;

    [Space]
    [Header("플레이어인가?")]
    public bool isPlayer = false;

    [Space]
    [Header("현재 상태이상")]
    public int weekness;
    public int fear;

    private Animator _animator;
    [SerializeField] private float effectSpeed = 0.005f;
    //private Color textColor = Color.white;

    private List<PoolType> currentStatus;

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

        if (isPlayer) maxHp = _battleUI.mainModule.playerDataSO.Health;
    }

    public void GetHit(int dmg, Color32 _color)
    {
        int realDmg = Mathf.Max(dmg - shield, 0);
        shield = Mathf.Max(shield - dmg, 0);

        if (weekness >= 1)
            realDmg = (int)(realDmg * 1.5f);

        hp = Mathf.Max(0, hp - realDmg);
        attackedEvent?.Invoke(realDmg);


        if (realDmg > 0) 
        { 
            _battleUI.SpawnSkillEffectText(realDmg.ToString(), _color, transform.position + new Vector3(0.5f, 1, -0.5f)); 
            
            GetDamaged();
        }
        else _battleUI.SpawnSkillEffectText("방어함", Color.white, transform.position + new Vector3(0.5f, 1, -0.5f));

        UpdateHPText();
        if (hp <= 0)
        {
            Dead();
        }

        _bar.fillAmount = (float)hp / maxHp;
    }
    public void GetHp(int value)
    {
        hp = Mathf.Min(hp + value, maxHp);
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
        _bar.fillAmount = (float)hp / maxHp;
    }

    public void SetAvtiveHpbar(bool isOn)
    {
        _hpbar.SetActive(isOn);
    }

    public void Dead()
    {
        deadEvent.Invoke();
        Debug.Log("으앙 죽음!");
    }

    public void InitHP(int _hp, int _max)
    {
        hp = _hp;
        maxHp = _max;
    }

    public void GetDamaged()
    {
        Sequence seq = DOTween.Sequence();
        if (isPlayer)
        {
            seq.Append(_battleUI._hitEffect.DOFade(0.6f, 0.1f));
            seq.Append(_battleUI._hitEffect.DOFade(0f, 0.25f));
        }
        transform.DOShakePosition(0.34f, 0.4f, 80);
        _animator.SetTrigger("GetHit");
    }

    public void OnShield(int value)
    {
        shield += value;
    }

    public void TurnEndAbnormalStatus()
    {
        if (weekness >= 1) weekness--;
        if (fear >= 1) fear--;


    }

}
