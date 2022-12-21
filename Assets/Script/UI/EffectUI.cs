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

    [Space]
    public TextMeshProUGUI _보;
    public TextMeshProUGUI _스;
    public TextMeshProUGUI _출;
    public TextMeshProUGUI _현;

    void Start()
    {
        
    }

    public void SetBattleEffect(bool isboss)
    {
        StartBattleEffectUI(isboss);
    }

    public void StartBattleEffectUI(bool isBoss)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(battleStartImage.transform.DOScale(1, 0.7f));
        if(isBoss)
        {
            seq.Join(_보.transform.DOLocalMoveX(-730, 0.2f));
            seq.Join(_스.transform.DOLocalMoveX(730, 0.2f));
            seq.Join(_출.transform.DOLocalMoveX(-730, 0.2f));
            seq.Join(_현.transform.DOLocalMoveX(730, 0.2f));
        }

        seq.Append(battleStartImage_1.transform.DOLocalMoveX(0, 0.5f));
        seq.Insert(0.7f, battleStartImage_2.transform.DOLocalMoveX(0, 0.5f));

        seq.Append(_battleText.transform.DOLocalMoveX(0, 0.5f));
        seq.Insert(1.4f, _startText.transform.DOLocalMoveX(0, 0.5f));

        seq.Append(_battleText.transform.DOShakePosition(0.3f, 20, 90));
        seq.Insert(2.1f, _startText.transform.DOShakePosition(0.3f, 20, 90));
        if (isBoss)
        {
            seq.Append(_보.transform.DOLocalMoveX(-1125, 0.1f));
            seq.Append(_스.transform.DOLocalMoveX(1130, 0.1f));
            seq.Append(_출.transform.DOLocalMoveX(-1125, 0.1f));
            seq.Append(_현.transform.DOLocalMoveX(1130, 0.1f));
        }

        seq.Append(_battleText.transform.DOLocalMoveX(1920, 0.5f));
        seq.Insert(2.8f, _startText.transform.DOLocalMoveX(-1920, 0.5f));


        seq.Append(battleStartImage_1.transform.DOLocalMoveX(1920, 0.5f));
        seq.Insert(3.5f, battleStartImage_2.transform.DOLocalMoveX(-1920, 0.5f));

        seq.Append(battleStartImage.transform.DOScale(1.1f, 0.5f));


        seq.AppendCallback(() => seq.Rewind());
        //seq.Rewind(true);
    }

    public void HitEffect()
    {

    }
}
