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

    public void SetText(int dmg, Color32 color)
    {
        Sequence seq = DOTween.Sequence();

        text.text = dmg.ToString();
        text.color = color;

        Vector2 pos = transform.position;
        pos.y += 10;

        seq.Append(transform.DOLocalMove(pos, 0.7f));
        seq.Join(text.DOFade(0, 1.2f));
    }
}
