using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TutorialManager : MonoSingleton<TutorialManager>
{
    public bool isTuto = false;
    public bool isInvenClear = false;
    public bool isDragItem = false;
    public bool isCardClick = false;

    public bool isItem;

    public bool isDragCard = false;

    public PlayerDataSO pdSO;

    public MainModule mainModule;

    private void Start()
    {
       
        if (isTuto)
        {
            isDragCard = false;

            Invoke("First", 14f);
        }
        else
        {
            isDragCard = true;

        }

    }
    public void First()
    {
        mainModule.canMove = true;
        pdSO.canBattle = false;
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("The Endless Tower�� ���Ű��� ȯ���մϴ�");
        });
        seq.AppendInterval(4f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("�տ� �ִ� ���ڸ� ������");
            mainModule.canMove = false;
        });
    }
    private void Update()
    {
        if (isInvenClear && isItem)
        {
            InvenItem();
            isInvenClear = false;
        }
        if (isDragItem && isTuto)
        {
            InvenCard();
            isDragItem = false;
        }
        if (isCardClick && isTuto)
        {
            BattleInfo();
            isCardClick = false;
        }
    }

    public void OpenChestCallBack()
    {
        DialogManager.Instance.ShowText("E Ű�� ���� �κ��丮�� ���� ��������");
        mainModule.canInven = true;
    }

    public void InvenItem()
    {
        DialogManager.Instance.ShowText("�κ��� ���� �������� �巡�� �� �����ϼ���");
        isItem = false;
    }

    public void InvenCard()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("���ϼ̾�� Ī���ؿ�!!");
        });
        seq.AppendInterval(2f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("��ü�� ī�带 Ŭ���� �ڽŸ��� ī�� ���� Ŀ�����ϼ���");

        });
    }
    public void BattleInfo()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("���ϼ̾�� Ī���ؿ�!!");
        });
        seq.AppendInterval(2f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("���� ������ �Ϸ� ���������?");
            pdSO.canBattle = true;
        });
        seq.AppendInterval(3f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("���� ���ƴٴϴٺ��� ���� �����ϰ� �˴ϴ�");
            pdSO.canBattle = true;
        });
    }

    public void Battle()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(5f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("�־��� ī�带 ������ �巡���Ͽ� �����ϼ���");
        });
        seq.AppendInterval(6f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("ī�� ��뿡 �ʿ��� �ڽ�Ʈ�� �ϸ��� ��� ä�����ϴ�");
        });
        seq.AppendInterval(6f);

        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("���� �ڽ�Ʈ�� ������ ���ʿ� ���� ������� �����մϴ�");
        });
        seq.AppendInterval(6f);

        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("�� �̻� ����� ���ִ� ī�尡 ���ų� �ڽ�Ʈ�� ������");
        });
        seq.AppendInterval(6f);

        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("Turn End ��ư�� ���� ���� �����մϴ�");
            isDragCard = true;
        });
    }
}
