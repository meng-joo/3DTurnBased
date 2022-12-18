using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EffectUI : MonoBehaviour
{
    public Image battleStartImage;
    public Image battleStartImage_1;
    public Image battleStartImage_2;
    public TextMeshProUGUI _battleText;
    public TextMeshProUGUI _startText;

    void Start()
    {
        
    }

    public void SetBattleEffect(bool isboss)
    {
        if (!isboss) StartBattleEffectUI();
        else
        {

        }
    }

    public void StartBattleEffectUI()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(battleStartImage.transform.DOScale(1, 0.7f));

        seq.Append(battleStartImage_1.transform.DOLocalMoveX(0, 0.7f));
        seq.Insert(0.7f, battleStartImage_2.transform.DOLocalMoveX(0, 0.7f));

        seq.Append(_battleText.transform.DOLocalMoveX(0, 0.7f));
        seq.Insert(1.4f, _startText.transform.DOLocalMoveX(0, 0.7f));

        seq.Append(_battleText.transform.DOShakePosition(0.3f, 20, 90));
        seq.Insert(2.1f, _startText.transform.DOShakePosition(0.3f, 20, 90));

        seq.Append(_battleText.transform.DOLocalMoveX(1920, 0.7f));
        seq.Insert(2.8f, _startText.transform.DOLocalMoveX(-1920, 0.7f));

        seq.Append(battleStartImage_1.transform.DOLocalMoveX(1920, 0.7f));
        seq.Insert(3.5f, battleStartImage_2.transform.DOLocalMoveX(-1920, 0.7f));

        seq.Append(battleStartImage.transform.DOScale(1.1f, 0.7f));

        seq.AppendCallback(() => seq.Rewind());
        //seq.Rewind(true);
    }

    public void HitEffect()
    {

    }
}