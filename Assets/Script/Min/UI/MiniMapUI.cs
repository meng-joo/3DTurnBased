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
    [Header("�̴ϸ� ī�޶�")]
    public Camera miniMapCamera;

    [Space]
    [Header("�÷��̾� ��ġUI")]
    public Image playerPosUI;

    [Space]
    [Header("�ð�")]
    public TextMeshProUGUI clockText;

    [Space]
    [Header("��")]
    public TextMeshProUGUI hpText;

    [Space]
    [Header("ī���ؽ�Ʈ")]
    public TextMeshProUGUI cardText;

    [Space]
    [Header("�÷��̾�")]
    public MainModule player;

    private void OnEnable()
    {
        InvokeRepeating("UpdateTime", 0, 0.4f);
        hpText.text = $"{player._HpModule.hp} / {player._HpModule.maxHp}";
        int count = player.playerDataSO._skills.Length + skills.cards.Count;
        cardText.text = count.ToString();
    }

    private void Update()
    {
        Vector3 vec = miniMapCamera.WorldToScreenPoint(player.transform.position);
        vec.z = 0;
        playerPosUI.transform.position = vec;
    }

    private void UpdateTime()
    {
        clockText.text = DateTime.Now.ToString("HH:mm");
    }
}
