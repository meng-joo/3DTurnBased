using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SkillEffectUI : PoolAbleObject
{
    TextMeshProUGUI text;
    public override void Init_Pop()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public override void Init_Push()
    {
    }

    public void SetText(string dmg, Color32 color, Vector3 vec)
    {
        Sequence seq = DOTween.Sequence();

        text.text = dmg;
        text.color = color;

        seq.Append(transform.DOMove(vec + new Vector3(0, 184, 0), 1f));
        seq.Append(text.DOFade(0, 1.2f));
    }
}