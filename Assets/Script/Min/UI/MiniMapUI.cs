using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MiniMapUI : MonoBehaviour
{
    public SkillIInvenObj skills;
    
    [Space]
    [Header("미니맵 카메라")]
    public Camera miniMapCamera;

    [Space]
    [Header("시계")]
    public TextMeshProUGUI clockText;

    [Space]
    [Header("피")]
    public TextMeshProUGUI hpText;

    [Space]
    [Header("카드텍스트")]
    public TextMeshProUGUI cardText;

    [Space]
    [Header("플레이어")]
    public MainModule player;

    private void OnEnable()
    {
        InvokeRepeating("UpdateTime", 0, 0.4f);
        hpText.text = $"{player._HpModule.hp} / {player._HpModule.maxHp}";
        int count = player.playerDataSO._skills.Count + skills.cards.Count;
        cardText.text = count.ToString();
    }

    private void UpdateTime()
    {
        clockText.text = DateTime.Now.ToString("HH:mm");
    }
}
